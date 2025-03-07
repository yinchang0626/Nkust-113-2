`timescale 1ns/10ps

`include "PATTERN.v"
// `include "SD.v"
// `ifdef RTL
//   `include "SD.v"
// `endif
// `ifdef GATE
//   `include "SD_SYN.v"
// `endif
	  		  	
module TESTBED;

wire clk, rst_n, in_valid;
wire [3:0] in;
wire out_valid;
wire [3:0] out;

wire [3:0] state;
wire [3:0] nstate;
wire [3:0] x;
wire [3:0] y;
wire [3:0] blank_cnt;
wire [7:0] blank0;
wire [7:0] blank1;
wire [7:0] blank2;
wire [7:0] blank3;
wire [7:0] blank4;
wire [7:0] blank5;
wire [7:0] blank6;
wire [7:0] blank7;
wire [7:0] blank8;
wire [7:0] blank9;
wire [7:0] blank10;
wire [7:0] blank11;
wire [7:0] blank12;
wire [7:0] blank13;
wire [7:0] blank14;
wire [11:0] stack0;
wire [11:0] stack1;
wire [11:0] stack2;
wire [11:0] stack3;
wire [11:0] stack4;
wire [11:0] stack5;
wire [11:0] stack6;
wire [11:0] stack7;
wire [11:0] stack8;
wire [11:0] stack9;
wire [11:0] stack10;
wire [11:0] stack11;
wire [11:0] stack12;
wire [11:0] stack13;
wire [11:0] stack14;
wire [9:0] L_to_R0;
wire [9:0] L_to_R1;
wire [9:0] L_to_R2;
wire [9:0] L_to_R3;
wire [9:0] L_to_R4;
wire [9:0] L_to_R5;
wire [9:0] L_to_R6;
wire [9:0] L_to_R7;
wire [9:0] L_to_R8;
wire [9:0] T_to_B0;
wire [9:0] T_to_B1;
wire [9:0] T_to_B2;
wire [9:0] T_to_B3;
wire [9:0] T_to_B4;
wire [9:0] T_to_B5;
wire [9:0] T_to_B6;
wire [9:0] T_to_B7;
wire [9:0] T_to_B8;
wire [9:0] CUBE0;
wire [9:0] CUBE1;
wire [9:0] CUBE2;
wire [9:0] CUBE3;
wire [9:0] CUBE4;
wire [9:0] CUBE5;
wire [9:0] CUBE6;
wire [9:0] CUBE7;
wire [9:0] CUBE8;
wire [3:0] stack_x;
wire [3:0] stack_y;
wire [3:0] stack_num;
wire [3:0] stack_num_plus1;
wire [9:0] cal_point;
wire [9:0] three_or_one;
wire [9:0] x_binary;
wire [9:0] input_binary;

initial begin
  `ifdef RTL
    $fsdbDumpfile("SD.fsdb");
  $fsdbDumpvars(0,"+mda");
  `endif
  `ifdef GATE
    $sdf_annotate("SD_SYN.sdf", u_SD);
    $fsdbDumpfile("SD_SYN.fsdb");
  $fsdbDumpvars(0,"+mda"); 
  `endif
end

SD u_SD(
    .clk(clk),
    .rst_n(rst_n),
    .in_valid(in_valid),
	.in(in),
    .out_valid(out_valid),
    .out(out),


    .state(state),
.nstate(nstate),
.x(x),
.y(y),
.blank_cnt(blank_cnt),
.blank0(blank0),
.blank1(blank1),
.blank2(blank2),
.blank3(blank3),
.blank4(blank4),
.blank5(blank5),
.blank6(blank6),
.blank7(blank7),
.blank8(blank8),
.blank9(blank9),
.blank10(blank10),
.blank11(blank11),
.blank12(blank12),
.blank13(blank13),
.blank14(blank14),
.stack0(stack0),
.stack1(stack1),
.stack2(stack2),
.stack3(stack3),
.stack4(stack4),
.stack5(stack5),
.stack6(stack6),
.stack7(stack7),
.stack8(stack8),
.stack9(stack9),
.stack10(stack10),
.stack11(stack11),
.stack12(stack12),
.stack13(stack13),
.stack14(stack14),
.L_to_R0(L_to_R0),
.L_to_R1(L_to_R1),
.L_to_R2(L_to_R2),
.L_to_R3(L_to_R3),
.L_to_R4(L_to_R4),
.L_to_R5(L_to_R5),
.L_to_R6(L_to_R6),
.L_to_R7(L_to_R7),
.L_to_R8(L_to_R8),
.T_to_B0(T_to_B0),
.T_to_B1(T_to_B1),
.T_to_B2(T_to_B2),
.T_to_B3(T_to_B3),
.T_to_B4(T_to_B4),
.T_to_B5(T_to_B5),
.T_to_B6(T_to_B6),
.T_to_B7(T_to_B7),
.T_to_B8(T_to_B8),
.CUBE0(CUBE0),
.CUBE1(CUBE1),
.CUBE2(CUBE2),
.CUBE3(CUBE3),
.CUBE4(CUBE4),
.CUBE5(CUBE5),
.CUBE6(CUBE6),
.CUBE7(CUBE7),
.CUBE8(CUBE8),
.stack_x(stack_x),
.stack_y(stack_y),
.stack_num(stack_num),
.stack_num_plus1(stack_num_plus1),
.cal_point(cal_point),
.three_or_one(three_or_one),
.x_binary(x_binary),
.input_binary(input_binary)
    );
	
PATTERN u_PATTERN(
    .clk(clk),
    .rst_n(rst_n),
    .in_valid(in_valid),
	.in(in),
    .out_valid(out_valid),
    .out(out)
    );
  
endmodule