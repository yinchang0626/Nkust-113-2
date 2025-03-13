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
reg [3:0] in_reg;

//================================================================
//  reg & wire definition                        
//================================================================
reg [3:0] state, nstate;
reg [3:0] x;
reg [3:0] y;

reg [3:0] blank_cnt;
reg [7:0] blank[14:0];
wire [3:0] stack_x;
wire [3:0] stack_y;
wire [3:0] stack_num;
wire [3:0] stack_num_plus1;

reg [11:0] stack[14:0]; // y[11:8] x[7:4] num[3:0]
wire [9:0] cal_point;

reg [9:0] L_to_R[8:0];
reg [9:0] T_to_B[8:0];
reg [9:0] CUBE[8:0];

wire [9:0] three_or_one;
wire [9:0] x_binary;
wire [9:0] input_binary;


reg [9:0] L_to_R_stack[8:0];
reg [9:0] T_to_B_stack[8:0];
reg [9:0] CUBE_stack[8:0];

reg wrong_graph;
wire [9:0] L_to_R_xor;
wire [9:0] T_to_B_xor;
wire [9:0] CUBE_xor;


integer i;


wire [3:0] stack_y_pre1;
wire [3:0] stack_x_pre1;
wire [3:0] blank_cnt_minus1;
wire [3:0] stack_num_pre1;

assign L_to_R_xor = L_to_R[y] ^ input_binary;
assign T_to_B_xor = T_to_B[x] ^ input_binary;
assign CUBE_xor   = CUBE[(y / 3) * 3 + x / 3] ^ input_binary;

assign blank_cnt_minus1 = (blank_cnt > 4'd0) ? blank_cnt - 4'd1 : blank_cnt;
assign stack_num_pre1 = stack[blank_cnt_minus1][3:0];

// assign stack_y = stack[blank_cnt][11:8];
// assign stack_x = stack[blank_cnt][7:4];
assign stack_y = blank[blank_cnt][7:4];
assign stack_x = blank[blank_cnt][3:0];
assign stack_y_pre1 =stack[blank_cnt_minus1][11:8];
assign stack_x_pre1 =stack[blank_cnt_minus1][7:4];

assign stack_num = stack[blank_cnt][3:0];
assign stack_num_plus1 = stack_num + 4'd1;
assign three_or_one = L_to_R[stack_y] | T_to_B[stack_x] | CUBE[(stack_y / 3) * 3 + stack_x / 3];
assign cal_point = three_or_one ^ x_binary;


int_to_binary u1(in_reg, input_binary);
int_to_binary u2(x, x_binary);


//======<FSM STATE>========//
parameter IDLE      = 4'd0;     // waiting for the read valid signal
parameter READ      = 4'd1;     // read 9 * 9 Sudoku number;
parameter R_DONE    = 4'd2;
parameter PUSH      = 4'd3;     // push the blank{y, x, num} to the stack
parameter CAL       = 4'd4;     // run x = 1 ~ 9 try;
parameter RIGHT     = 4'd5;     // if this x can push to this blank
parameter POP       = 4'd6;     // if this number is can't push this blank, it need to pop out this stack
parameter WRONG     = 4'd7;     // if this 
parameter GOOD      = 4'd8;
parameter END       = 4'd9;

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
        READ:       nstate = (y == 4'd9 && x == 4'd0) ? R_DONE : READ;
        R_DONE:     nstate = (wrong_graph == 1'b1) ? WRONG : CAL;
        PUSH:       nstate = CAL;
        CAL:        nstate = (cal_point > three_or_one) ? RIGHT :
                             (blank_cnt == 4'd0 && x >= 4'd10) ? WRONG :
                             (x == 4'd9 || stack_num >= 4'd9) ? POP : CAL;
        RIGHT:      nstate = (blank_cnt == 4'd14) ? GOOD : PUSH;
        POP:        nstate = (blank_cnt == 4'd0) ? WRONG :
                                     (x == 4'd9) ? POP : PUSH;
        WRONG:      nstate = END;
        GOOD:       nstate = (y == 4'd14) ? END : GOOD;
        END:        nstate = IDLE;
        default: nstate = IDLE;
    endcase
end


always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        x <= 4'd0;
        y <= 4'd0;
        in_reg <= 4'd0;
    end
    else begin
        case(state)
            IDLE:begin
                in_reg <= in;
            end
            READ:begin
                if (x == 4'd8)begin
                    x <= 4'd0;
                    y <= y + 4'd1;
                end
                else begin
                    x <= x + 4'd1;
                end
                in_reg <= in;
            end
            R_DONE:begin
                x <= 4'd0;
                y <= 4'd0;
            end
            PUSH:begin
                y <= 4'd0;
                x <= stack_num_plus1;
            end
            CAL:begin
                x <= (cal_point > three_or_one) ? x : 
                      (x < 4'd9) ? x + 4'd1 : 4'd0;
            end
            GOOD:begin
                y <= y + 4'd1;
            end
            END:begin
                x <= 4'd0;
                y <= 4'd0;
                in_reg <= 4'd0;
            end
        endcase
    end
end


always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (i = 0; i <= 8; i = i + 1) begin
            L_to_R[i] <= 10'd0;
            T_to_B[i] <= 10'd0;
            CUBE[i] <= 10'd0;
            wrong_graph <= 1'b0;
        end
    end
    else begin
        case(state)
            READ:begin
                if ((L_to_R_xor < L_to_R[y] || T_to_B_xor < T_to_B[x] ||  CUBE_xor < CUBE[(y / 3) * 3 + x / 3]) && input_binary != 10'd0)begin
                    wrong_graph <= 1'b1;
                end
                L_to_R[y] <= input_binary | L_to_R[y];
                T_to_B[x] <= input_binary | T_to_B[x];
                CUBE[(y / 3) * 3 + x / 3] <= input_binary | CUBE[(y / 3) * 3 + x / 3];
            end
            RIGHT:begin
                L_to_R[stack_y][x] <= 1'b1;
                T_to_B[stack_x][x] <= 1'b1;
                CUBE[(stack_y / 3) * 3 + stack_x / 3][x] <= 1'b1;
            end
            PUSH:begin
                L_to_R[stack_y][stack_num] <= 1'b0;
                T_to_B[stack_x][stack_num] <= 1'b0;
                CUBE[(stack_y / 3) * 3 + stack_x / 3][stack_num] <= 1'b0;
            end
            POP:begin
                L_to_R[stack_y_pre1][stack_num_pre1] <= (x == 4'd9) ? L_to_R[stack_y_pre1][stack_num_pre1] : 1'b0;
                T_to_B[stack_x_pre1][stack_num_pre1] <= (x == 4'd9) ? T_to_B[stack_x_pre1][stack_num_pre1] : 1'b0;
                CUBE[(stack_y_pre1 / 3) * 3 + stack_x_pre1 / 3][stack_num_pre1] <= (x == 4'd9) ? CUBE[(stack_y_pre1 / 3) * 3 + stack_x_pre1 / 3][stack_num_pre1] : 1'b0;
            end
            END:begin
                for (i = 0; i <= 8; i = i + 1) begin
                    L_to_R[i] <= 10'd0;
                    T_to_B[i] <= 10'd0;
                    CUBE[i] <= 10'd0;
                end
                wrong_graph <= 1'b0;
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (i = 0; i <= 14; i = i + 1) begin
            blank[i] <= 8'd0;
        end
        blank_cnt <= 4'd0;
    end
    else begin
        case(state)
            IDLE:begin
                if (in_valid == 1 && in_reg == 4'd0)begin
                    blank[blank_cnt] <= {y, x};
                end
            end
            READ:begin
                if (in_valid == 1 && in_reg == 4'd0)begin
                    blank[blank_cnt] <= {y, x};
                    blank_cnt <= (blank_cnt < 4'd14) ? blank_cnt + 4'd1 : blank_cnt;
                end
            end
            R_DONE:begin
                blank_cnt <= 4'd0;
            end
            RIGHT:begin
                blank_cnt <= (blank_cnt < 4'd14) ? blank_cnt + 4'd1 : blank_cnt;
            end
            POP:begin
                blank_cnt <= (blank_cnt > 4'd0) ? blank_cnt - 4'd1 : blank_cnt;
            end
            END:begin
                for (i = 0; i <= 14; i = i + 1) begin
                    blank[i] <= 8'd0;
                end
                blank_cnt <= 4'd0;
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (i = 0; i <= 14; i = i + 1) begin
            stack[i] <= 12'd0;
        end
    end
    else begin
        case(state)
            PUSH:begin
                stack[blank_cnt] <= {blank[blank_cnt], stack[blank_cnt][3:0]};
            end
            RIGHT:begin
                stack[blank_cnt] <= {blank[blank_cnt], x};
            end
            CAL:begin
                stack[blank_cnt] <= (cal_point > three_or_one) ? {blank[blank_cnt], x} : stack[blank_cnt];
            end
            POP:begin
                stack[blank_cnt] <= 12'd0;
            end
            END:begin
                for (i = 0; i <= 14; i = i + 1) begin
                    stack[i] <= 12'd0;
                end
            end
        endcase
    end
end

always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        out_valid <= 1'b0;
        out <= 4'd0;
    end
    else begin
        case(state)
            WRONG:begin
                out_valid <= 1'b1;
                out       <= 4'd10;
            end
            GOOD:begin
                out_valid <= 1'b1;
                out       <= stack[y][3:0];
            end
            default:begin
                out_valid <= 1'b0;
                out <= 4'd0;
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