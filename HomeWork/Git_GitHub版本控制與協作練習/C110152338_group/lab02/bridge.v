//############################################################################
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//   (C) Copyright Laboratory System Integration and Silicon Implementation
//   All Right Reserved
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
//   2023 ICLAB Fall Course
//   Lab03      : BRIDGE
//   Author     : CHEN, KE-RONG
//                
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
//   File Name   : BRIDGE_encrypted.v
//   Module Name : BRIDGE
//   Release version : v1.0 (Release Date: Sep-2023)
//
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//############################################################################

module bridge(
    // Input Signals
    clk,
    rst_n,
    in_valid,
    direction,
    addr_dram,
    addr_sd,

    // Output Signals
    out_valid,
    out_data,

    // DRAM Signals
    AR_VALID, AR_ADDR, R_READY, AW_VALID, AW_ADDR, W_VALID, W_DATA, B_READY,
    AR_READY, R_VALID, R_RESP, R_DATA, AW_READY, W_READY, B_VALID, B_RESP,

    // SD Signals
    MISO,
    MOSI
);

// Input Signals
input clk, rst_n;
input in_valid;
input direction;
input [12:0] addr_dram;
input [15:0] addr_sd;

// Output Signals
output reg out_valid;
output reg [7:0] out_data;

// DRAM Signals
// write address channel
output reg [31:0] AW_ADDR;
output reg AW_VALID;
input AW_READY;

// write data channel
output reg W_VALID;
output reg [63:0] W_DATA;
input W_READY;

// write response channel
input B_VALID;
input [1:0] B_RESP;
output reg B_READY;

// read address channel
output reg [31:0] AR_ADDR;
output reg AR_VALID;
input AR_READY;

// read data channel
input [63:0] R_DATA;
input R_VALID;
input [1:0] R_RESP;
output reg R_READY;

// SD Signals
input MISO;
output reg MOSI;

//==============================================//
//       parameter & integer declaration        //
//==============================================//

parameter READ             = 4'd0; //direction 0 DRAM->SD
parameter DRAM_READ_V      = 4'd1; 
parameter DRAM_READ_R      = 4'd2; 
parameter SD_COMMAND       = 4'd3;
parameter SD_RESPONSE      = 4'd4;
parameter SD_DATA_WRITE    = 4'd5;
parameter SD_RESPONSE2     = 4'd6;
parameter SD_DATA_READ     = 4'd7;
parameter DRAM_WRITE_V     = 4'd8;
parameter DRAM_WRITE_R     = 4'd9;
parameter DRAM_RESPONSE    = 4'd10;
parameter FINISH           = 4'd15;

//==============================================//
//            FSM State Declaration             //
//==============================================//
reg [3:0] state, nstate;


//==============================================//
//           reg & wire declaration             //
//==============================================//
reg        direction_reg;
reg [12:0] addr_dram_reg;
reg [15:0] addr_sd_reg;


reg [63:0] R_DATA_reg;
reg [83:0] sd_write_data;

//write command to SD
reg [39:0] CRC7_IDATA;
wire [47:0] SD_rw_cmd;
reg [87:0] SD_rw_data;
reg [10:0] cnt;


//==============================================//
//           CRC Calculation Module             //
//==============================================//
// CRC-7 function
function automatic [6:0] CRC7;
    input [39:0] data;  
    reg [6:0] crc;
    integer i;
    reg data_in, data_out;
    parameter polynomial = 7'h9;

    begin
        crc = 7'd0;
        for (i = 0; i < 40; i = i + 1) begin
            data_in = data[39-i];
            data_out = crc[6];
            crc = crc << 1;  // Shift the CRC
            if (data_in ^ data_out) begin
                crc = crc ^ polynomial;
            end
        end
        CRC7 = crc;
    end
endfunction

// CRC-16-CCITT function
function automatic [15:0] CRC16_CCITT; 
    input [63:0] data;
    reg [15:0] crc;
    integer i;
    reg data_in, data_out;
    parameter polynomial = 16'h1021;  // x^16 + x^12 + x^5 + 1
    
    begin
        crc = 16'd0;
        for (i = 0; i < 64; i = i + 1) begin
            data_in = data[63-i];
            data_out = crc[15];
            crc = crc << 1;  // Shift the CRC
            if (data_in ^ data_out) begin
                crc = crc ^ polynomial;
            end
        end
        CRC16_CCITT = crc;
    end
endfunction

//==============================================//
//             FSM State Transition             //
//==============================================//
always @(posedge clk, negedge rst_n) begin
    if (!rst_n) begin
        state <= READ;
    end
    else begin
        state <= nstate;
    end
end


//==============================================//
//              Next State Block                //
//==============================================//
always@(*)begin
    case(state)
        READ:         nstate = (in_valid == 1&& direction == 0) ? DRAM_READ_V : SD_READ;
        DRAM_READ_V:  nstate = (AR_READY) ? DRAM_READ_R : DRAM_READ_V;
        DRAM_READ_R:  nstate = (R_VALID)  ? SD_COMMAND : DRAM_READ_R;
        SD_COMMAND:   nstate = (cnt == 11'd47) ? SD_RESPONSE : SD_COMMAND;
        SD_RESPONSE:  nstate = (cnt == 11'd7 && direction_reg == 1) ? SD_DATA_READ :
                               (cnt == 11'd7 && direction_reg == 0) ? SD_DATA_WRITE
                                                                    : SD_RESPONSE;
        SD_DATA_WRITE:nstate = (cnt == 11'd87) ? SD_RESPONSE2 : SD_DATA_WRITE;
        SD_RESPONSE2: nstate = (cnt > 11'd8 && MISO == 1) ? FINISH : SD_RESPONSE;
        SD_DATA_READ: nstate = (cnt == 11'd87) ? DRAM_WRITE_V : SD_DATA_READ;
        DRAM_WRITE_V: nstate = (AW_READY) ? DRAM_WRITE_R : DRAM_WRITE_V;
        DRAM_WRITE_R: nstate = (W_VALID)  ? DRAM_RESPONSE : DRAM_WRITE_R;
        DRAM_RESPONSE:nstate = (B_VALID) ? FINISH : DRAM_RESPONSE;
        FINISH:       nstate = FINISH;
        default:      nstate = FINISH;
    endcase
end



//==============================================//
//                Read PATTERN                  //
//==============================================//
always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        {direction_reg, addr_dram_reg, addr_sd_reg} <= 27'd0;
    end
    else begin
        case(state)
            READ : begin
                if (in_valid == 1) begin
                    {direction_reg, addr_dram_reg, addr_sd_reg} <= {direction, addr_dram, addr_sd};
                end
            end
        endcase
    end
end

//==============================================//
//                DARM Read                     //
//==============================================//
always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        AR_ADDR  <= 32'd0;
        AR_VALID <= 1'd0;
        R_READY  <= 1'd0;
    end
    else begin
        case(state)
            DRAM_READ_V:begin
                AR_ADDR  <= addr_dram_reg;
                AR_VALID <= 1'd1;
            end
            DRAM_READ_R:begin
                AR_ADDR  <= 32'd0;
                AR_VALID <= 1'd0;
                R_READY <= 1'd1;
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        R_DATA_reg <= 64'd0;
    end
    else begin
        case(state)
            DRAM_READ_R : R_DATA_reg <= R_DATA;
        endcase
    end
end

//==============================================//
//                DARM Write                    //
//==============================================//
always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        AW_ADDR  <= 32'd0;
        AW_VALID <= 1'd0;
        W_READY  <= 1'd0;
        B_READY  <= 1'd0;
    end
    else begin
        case(state)
            DRAM_WRITE_V:begin
                AW_ADDR  <= addr_dram_reg;
                AW_VALID <= 1'd1;
            end
            DRAM_WRITE_R:begin
                AW_ADDR  <= 32'd0;
                AW_VALID <= 1'd0;
                W_READY <= 1'd1;
            end
            DRAM_RESPONSE:begin
                B_READY <= 1'b1;
            end
            default:begin
                AW_ADDR  <= 32'd0;
                AW_VALID <= 1'd0;
                W_READY  <= 1'd0;
                B_READY  <= 1'd0;
            end
        endcase
    end
end

assign W_DATA = SD_rw_data[72:16];


//==============================================//
//             Write Command to SD             //
//==============================================//
always@(*)begin
    CRC7_IDATA = (direction_reg) ? {2'b01, 6'd17, addr_sd_reg} 
                                 : {2'b01, 6'd24, addr_sd_reg};
end

assign SD_rw_cmd = {CRC7_IDATA, CRC7(CRC7_IDATA), 1'b1};



always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        SD_rw_data <= 88'd0;
    end
    else begin
        case(state)
            SD_DATA_WRITE: SD_rw_data <= {8'hFE, R_DATA_reg, CRC16_CCITT(R_DATA_reg)};
            SD_DATA_READ:  SD_rw_data <= {MISO, SD_rw_data[86:1]};
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        MISO <= 1'b1;
    end
    else begin
        case(state)
            SD_COMMAND:         MISO <= SD_rw_cmd[11'd48 - cnt];
            SD_DATA_WRITE:      MISO <= SD_rw_data[11'd87 - cnt];
            default:            MISO <= 1'd1;
        endcase
    end
end



always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        cnt <= 11'd0;
    end
    else begin
        case(state)
            SD_COMMAND:    cnt <= cnt + 11'd1;
            SD_DATA_WRITE: cnt <= cnt + 11'd1;
            SD_RESPONSE2:  cnt <= cnt + 11'd1;
            default:       cnt <= 11'd0;
        endcase
    end
end

endfunction