module DIV(clk, reset, num, den, out, valid);
	parameter SIZE = 8'd96;
	parameter DEC_SIZE = 8'd32;
	parameter LOG2 = 4'd10;
	input clk;
	input reset;
	input [SIZE - 1:0] num, den;
	output [SIZE - 1:0] out;
	output valid;

	reg [SIZE - 1:0] quo;
	reg [SIZE - 1:0] rem;
	wire [SIZE - 1:0] minus;
	reg [LOG2 - 1:0] cnt;

	reg [1:0] state, nstate;
	parameter IDLE = 2'd0;
	parameter RES  = 2'd1;
	parameter RUN  = 2'd2;
	parameter DONE = 2'd3;
	
	assign valid = (state == DONE) ? 1'b1 : 1'b0;
	assign minus = rem - den;
	assign out = quo;
	
	always@(posedge clk, posedge reset)begin
		if(reset)begin
			state <= IDLE;
		end
		else begin
			state <= nstate;
		end
	end

	always@(*)begin
		case(state)
			IDLE: nstate = RES;
			RES:  nstate = RUN;
			RUN:  nstate = (cnt == (SIZE + DEC_SIZE)) ? DONE : RUN;
			DONE: nstate = DONE;
			default: nstate = IDLE;
		endcase
	end

	always@(posedge clk,posedge reset)begin
		if(reset)begin
			quo <= 0;
			rem <= 0;
		end
		else begin
			case(state)
				RES : begin
					quo <= num;
					rem <= 0;
				end
				RUN:begin
					if(den > rem)begin
						{rem, quo} <= {rem, quo, 1'b0};
					end
					else begin
						{rem, quo} <= {minus, quo, 1'b1};
					end
				end
			endcase
		end
	end
	
	always@(posedge clk, posedge reset)begin
		if(reset)begin
			cnt <= 0;
		end
		else begin
			case(state)
				RUN:	cnt <= cnt + 1;
				default: cnt <= 0;
			endcase
		end
	end

endmodule 