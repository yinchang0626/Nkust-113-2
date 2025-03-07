module DIV(clk,rst,a,b,c,d);
input clk,rst;
input [7:0] a,b;
output reg [7:0] c,d;

reg [3:0] run_time;
reg [7:0] data_c,data_d;

always @(negedge clk,posedge rst)begin
	if(rst)begin
		c=8'b0000_0000;			//quotient
		d=8'b0000_0000;			//remainder
		run_time=4'b0000;		//time reset
	end
	else begin
		if(run_time==0)begin
				data_c=a;					//put a in quotient
				data_d=8'b0000_0000;	//clear remainder
				run_time=run_time+1'b1;
		end
		else if(run_time<=9)begin
			if(b>data_d)begin
				{data_d,data_c}=({data_d,data_c}<<1);
			end
			else begin
				data_d=(data_d-b);
				{data_d,data_c}=({data_d,data_c}<<1);
				data_c[0]=1'b1;
			end
			run_time=run_time+1'b1;
		end
		else if(run_time==10)begin
			data_d=data_d>>1;
			c=data_c;
			d=data_d;
			run_time=run_time+1'b1;
		end
		else begin
			run_time=4'b0000;
		end
	end
end

endmodule 