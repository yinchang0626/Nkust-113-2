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
    out,



    state,
nstate,
x,
y,
blank_cnt,
blank0,
blank1,
blank2,
blank3,
blank4,
blank5,
blank6,
blank7,
blank8,
blank9,
blank10,
blank11,
blank12,
blank13,
blank14,
stack0,
stack1,
stack2,
stack3,
stack4,
stack5,
stack6,
stack7,
stack8,
stack9,
stack10,
stack11,
stack12,
stack13,
stack14,
L_to_R0,
L_to_R1,
L_to_R2,
L_to_R3,
L_to_R4,
L_to_R5,
L_to_R6,
L_to_R7,
L_to_R8,
T_to_B0,
T_to_B1,
T_to_B2,
T_to_B3,
T_to_B4,
T_to_B5,
T_to_B6,
T_to_B7,
T_to_B8,
CUBE0,
CUBE1,
CUBE2,
CUBE3,
CUBE4,
CUBE5,
CUBE6,
CUBE7,
CUBE8,
stack_x,
stack_y,
stack_num,
stack_num_plus1,
cal_point,
three_or_one,
x_binary,
input_binary
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
// reg [3:0] state, nstate;
// reg [3:0] x;
// reg [3:0] y;

// reg [3:0] blank_cnt;
// reg [7:0] blank[14:0];
// wire [3:0] stack_x;
// wire [3:0] stack_y;
// wire [3:0] stack_num;
// wire [3:0] stack_num_plus1;

// reg [11:0] stack[14:0]; // y[11:8] x[7:4] num[3:0]
// wire [9:0] cal_point;

// reg [9:0] L_to_R[8:0];
// reg [9:0] T_to_B[8:0];
// reg [9:0] CUBE[8:0];

// wire [9:0] three_or_one;
// wire [9:0] x_binary;
// wire [9:0] input_binary;

output reg [3:0] state, nstate;
output reg [3:0] x;
output reg [3:0] y;
output reg [3:0] blank_cnt;
output [7:0] blank0;
output [7:0] blank1;
output [7:0] blank2;
output [7:0] blank3;
output [7:0] blank4;
output [7:0] blank5;
output [7:0] blank6;
output [7:0] blank7;
output [7:0] blank8;
output [7:0] blank9;
output [7:0] blank10;
output [7:0] blank11;
output [7:0] blank12;
output [7:0] blank13;
output [7:0] blank14;
output [11:0] stack0;
output [11:0] stack1;
output [11:0] stack2;
output [11:0] stack3;
output [11:0] stack4;
output [11:0] stack5;
output [11:0] stack6;
output [11:0] stack7;
output [11:0] stack8;
output [11:0] stack9;
output [11:0] stack10;
output [11:0] stack11;
output [11:0] stack12;
output [11:0] stack13;
output [11:0] stack14;
output [9:0] L_to_R0;
output [9:0] L_to_R1;
output [9:0] L_to_R2;
output [9:0] L_to_R3;
output [9:0] L_to_R4;
output [9:0] L_to_R5;
output [9:0] L_to_R6;
output [9:0] L_to_R7;
output [9:0] L_to_R8;
output [9:0] T_to_B0;
output [9:0] T_to_B1;
output [9:0] T_to_B2;
output [9:0] T_to_B3;
output [9:0] T_to_B4;
output [9:0] T_to_B5;
output [9:0] T_to_B6;
output [9:0] T_to_B7;
output [9:0] T_to_B8;
output [9:0] CUBE0;
output [9:0] CUBE1;
output [9:0] CUBE2;
output [9:0] CUBE3;
output [9:0] CUBE4;
output [9:0] CUBE5;
output [9:0] CUBE6;
output [9:0] CUBE7;
output [9:0] CUBE8;
output [3:0] stack_x;
output [3:0] stack_y;
output [3:0] stack_num;
output [3:0] stack_num_plus1;
output [9:0] cal_point;
output [9:0] three_or_one;
output [9:0] x_binary;
output [9:0] input_binary;

reg [7:0] blank[14:0];
reg [11:0] stack[14:0]; // y[11:8] x[7:4] num[3:0]
reg [9:0] L_to_R[8:0];
reg [9:0] T_to_B[8:0];
reg [9:0] CUBE[8:0];


assign blank0 = blank[0];
assign blank1 = blank[1];
assign blank2 = blank[2];
assign blank3 = blank[3];
assign blank4 = blank[4];
assign blank5 = blank[5];
assign blank6 = blank[6];
assign blank7 = blank[7];
assign blank8 = blank[8];
assign blank9 = blank[9];
assign blank10 = blank[10];
assign blank11 = blank[11];
assign blank12 = blank[12];
assign blank13 = blank[13];
assign blank14 = blank[14];





assign stack0 = stack[0];
assign stack1 = stack[1];
assign stack2 = stack[2];
assign stack3 = stack[3];
assign stack4 = stack[4];
assign stack5 = stack[5];
assign stack6 = stack[6];
assign stack7 = stack[7];
assign stack8 = stack[8];
assign stack9 = stack[9];
assign stack10 = stack[10];
assign stack11 = stack[11];
assign stack12 = stack[12];
assign stack13 = stack[13];
assign stack14 = stack[14];



assign L_to_R0 = L_to_R[0];
assign L_to_R1 = L_to_R[1];
assign L_to_R2 = L_to_R[2];
assign L_to_R3 = L_to_R[3];
assign L_to_R4 = L_to_R[4];
assign L_to_R5 = L_to_R[5];
assign L_to_R6 = L_to_R[6];
assign L_to_R7 = L_to_R[7];
assign L_to_R8 = L_to_R[8];
assign T_to_B0 = T_to_B[0];
assign T_to_B1 = T_to_B[1];
assign T_to_B2 = T_to_B[2];
assign T_to_B3 = T_to_B[3];
assign T_to_B4 = T_to_B[4];
assign T_to_B5 = T_to_B[5];
assign T_to_B6 = T_to_B[6];
assign T_to_B7 = T_to_B[7];
assign T_to_B8 = T_to_B[8];
assign CUBE0 = CUBE[0];
assign CUBE1 = CUBE[1];
assign CUBE2 = CUBE[2];
assign CUBE3 = CUBE[3];
assign CUBE4 = CUBE[4];
assign CUBE5 = CUBE[5];
assign CUBE6 = CUBE[6];
assign CUBE7 = CUBE[7];
assign CUBE8 = CUBE[8];


integer i;

assign stack_y = stack[blank_cnt][11:8];
assign stack_x = stack[blank_cnt][7:4];
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
        R_DONE:     nstate = CAL;
        PUSH:       nstate = CAL;
        CAL:        nstate = (cal_point > three_or_one) ? RIGHT :
                             (blank_cnt == 4'd0 && x >= 4'd10) ? WRONG :
                             (x == 4'd9 || stack_num >= 4'd9) ? POP : CAL;
        RIGHT:      nstate = (blank_cnt == 4'd15) ? GOOD : PUSH;
        POP:        nstate = PUSH;
        WRONG:      nstate = END;
        GOOD:       nstate = (y == 4'd9) ? END : GOOD;
        END:        nstate = END;
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
        endcase
    end
end


always@(posedge clk, negedge rst_n)begin
    if(!rst_n)begin
        for (i = 0; i <= 8; i = i + 1) begin
            L_to_R[i] <= 10'd0;
            T_to_B[i] <= 10'd0;
            CUBE[i] <= 10'd0;
        end
    end
    else begin
        case(state)
            READ:begin
                L_to_R[y] <= input_binary | L_to_R[y];
                T_to_B[x] <= input_binary | T_to_B[x];
                CUBE[(y / 3) * 3 + x / 3] <= input_binary | CUBE[(y / 3) * 3 + x / 3];
            end
            RIGHT:begin
                L_to_R[stack_y][stack_num] <= 1'b1;
                T_to_B[stack_x][stack_num] <= 1'b1;
                CUBE[(stack_y / 3) * 3 + stack_x / 3][stack_num] <= 1'b1;
            end
            POP:begin
                L_to_R[stack_y][stack_num] <= 1'b0;
                T_to_B[stack_x][stack_num] <= 1'b0;
                CUBE[(stack_y / 3) * 3 + stack_x / 3][stack_num] <= 1'b0;
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
            PUSH:begin
                blank_cnt <= (blank_cnt < 4'd14) ? blank_cnt + 4'd1 : blank_cnt;
            end
            POP:begin
                blank_cnt <= (blank_cnt > 4'd0) ? blank_cnt - 4'd1 : blank_cnt;
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
                stack[(blank_cnt + 4'd1)] <= {blank[(blank_cnt + 4'd1)], stack_num_plus1};
            end
            CAL:begin
                stack[blank_cnt] <= (cal_point > three_or_one) ? {blank[blank_cnt], x} : stack[blank_cnt];
            end
            POP:begin
                stack[blank_cnt] <= 12'd0;
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