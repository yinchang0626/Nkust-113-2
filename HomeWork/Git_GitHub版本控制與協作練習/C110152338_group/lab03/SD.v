//############################################################################
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//   2021 ICLAB Spring Course
//   Lab03          : Sudoku (SD)
//   Author         : CHEN, KE-RONG
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//   File Name   : SD.v
//   Module Name : SD
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//############################################################################

module SD(
    // Input signals
    clk,
    rst_n,
    in_valid,
    in,
    // Output signals
    out_valid,
    out
);
//================================================================
//  INPUT AND OUTPUT DECLARATION                         
//================================================================
input clk, rst_n, in_valid;
input [3:0] in;
output reg out_valid;
output reg [3:0] out;

reg [2:0] state, nstate;
reg [3:0] x;
reg [3:0] y;

reg [3:0] blank_cnt;
reg [7:0] blank[14:0];
wire [3:0] stack_x;
wire [3:0] stack_y;

reg [11:0] stack[14:0]; // y[11:8] x[7:4] num[3:0]
wire [9:0] cal_point;

reg [9:0] L_to_R[8:0];
reg [9:0] T_to_B[8:0];
reg [9:0] CUBE[8:0];

wire [9:0] three_or_one;
wire [9:0] input_binary;
wire [9:0] x_binary;

assign stack_y = stack[blank_cnt][11:8];
assign stack_x = stack[blank_cnt][7:4];

assign three_or_one = L_to_R[stack_y] | T_to_B[stack_x] | CUBE[(stack_y / 3) * 3 + stack_x / 3];
assign cal_point = three_or_one ^ x_binary;
int_to_binary u1(in, input_binary);
int_to_binary u2(x, x_binary);

//======<FSM STATE>========//
parameter IDLE      = 3'd0;
parameter READ      = 3'd1;
parameter PUSH      = 3'd2;
parameter CAL       = 3'd3;
parameter RIGHT     = 3'd4;
parameter POP       = 3'd5;
parameter END       = 3'd7;

//======<RTL transmission>========//
always @(posedge clk, negedge rst_n) begin
    if(!rst_n)begin
        state <= IDLE;
    end
    else begin
        state <= nstate;
    end
end

//======<Next state block>========//
always @(*) begin
    case(state)
        IDLE:       nstate = (in_valid) ? READ: IDLE;
        READ:       nstate = (y == 4'd8 && x == 4'd8) ? PUSH : READ;
        PUSH:       nstate = CAL;
        CAL:        nstate = (cal_point > three_or_one && blank_cnt == 4'd15) ? END :
                             (cal_point > three_or_one) ? RIGHT :
                             (x == 4'd9) ? POP : CAL;
        RIGHT:      nstate = PUSH;
        POP:        nstate = PUSH;
        END:        nstate = END;
        default: nstate = IDLE;
    endcase
end


always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        x <= 4'd0;
        y <= 4'd0;
    end
    else begin
        case(state)
            READ:begin
                if (x == 4'd8)begin
                    x <= 4'd0;
                    y <= y + 4'd1;
                end
                else begin
                    x <= x + 4'd1;
                end
            end
            PUSH:begin
                y <= 4'd0;
                x <= stack[blank_cnt][3:0];
            end
            CAL:begin
                x <= x + 4'd1;
            end
        endcase
    end
end

integer i;
always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (int i = 0; i <= 8; i = i + 1) begin
            L_to_R[i] <= 10'd0;
            T_to_B[i] <= 10'd0;
            CUBE[i] <= 10'd0;
        end
    end
    else begin
        case(state)
            READ:begin
                L_to_R[y] <= (in_valid) ? input_binary | L_to_R[y] : L_to_R[y];
                T_to_B[x] <= (in_valid) ? input_binary | T_to_B[x] : T_to_B[x];
                CUBE[(y / 3) * 3 + x / 3] <= (in_valid) ? input_binary | CUBE[(y / 3) * 3 + x / 3] : CUBE[(y / 3) * 3 + x / 3];
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (int i = 0; i <= 14; i = i + 1) begin
            blank[i] <= 8'd0;
        end
        blank_cnt <= 4'd0;
    end
    else begin
        case(state)
            READ:begin
                if (in == 4'd0)begin
                    blank[blank_cnt] <= {y, x};
                    blank_cnt <= blank_cnt + 4'd1;
                end
            end
            RIGHT:begin
                blank_cnt <= blank_cnt + 4'd1;
            end
            POP:begin
                blank_cnt <= blank_cnt - 4'd1;
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (int i = 0; i <= 14; i = i + 1) begin
            stack[i] <= 12'd0;
        end
    end
    else begin
        case(state)
            PUSH:begin
                stack[blank_cnt] <= {blank[blank_cnt], (blank[blank_cnt][3:0] + 4'd1)};
            end
            POP:begin
                stack[blank_cnt] <= 12'd0;
            end
        endcase
    end
end



endmodule

module int_to_binary(in, out);

input [3:0] in;
output reg [9:0] out;

always@(*)begin
    case(in)
        4'd0:   out <= 10'b0_0000_0000_0;
        4'd1:   out <= 10'b0_0000_0001_0;
        4'd2:   out <= 10'b0_0000_0010_0;
        4'd3:   out <= 10'b0_0000_0100_0;
        4'd4:   out <= 10'b0_0000_1000_0;
        4'd5:   out <= 10'b0_0001_0000_0;
        4'd6:   out <= 10'b0_0010_0000_0;
        4'd7:   out <= 10'b0_0100_0000_0;
        4'd8:   out <= 10'b0_1000_0000_0;
        4'd9:   out <= 10'b1_0000_0000_0;
        default:out <= 10'b0_0000_0000_0;
    endcase
end

endmodule