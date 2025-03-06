module SMC(
    // Input Ports
    mode,
    W_0, V_GS_0, V_DS_0,
    W_1, V_GS_1, V_DS_1,
    W_2, V_GS_2, V_DS_2,
    W_3, V_GS_3, V_DS_3,
    W_4, V_GS_4, V_DS_4,
    W_5, V_GS_5, V_DS_5,

    // Output Ports
    out_n
);

input [2:0] W_0, V_GS_0, V_DS_0;
input [2:0] W_1, V_GS_1, V_DS_1;
input [2:0] W_2, V_GS_2, V_DS_2;
input [2:0] W_3, V_GS_3, V_DS_3;
input [2:0] W_4, V_GS_4, V_DS_4;
input [2:0] W_5, V_GS_5, V_DS_5;
input [1:0] mode;


output [7:0] out_n;     

wire [7:0] ID0_b, ID1_b, ID2_b, ID3_b, ID4_b, ID5_b;
wire [7:0] gm0_b, gm1_b, gm2_b, gm3_b, gm4_b, gm5_b;

wire [7:0] sort_0, sort_1, sort_2, sort_3, sort_4, sort_5;

wire [7:0] nn1, nn2, nn3;
wire [9:0] ans;


    calculate c0(ID0_b, gm0_b, W_0, V_GS_0, V_DS_0);
    calculate c1(ID1_b, gm1_b, W_1, V_GS_1, V_DS_1);
    calculate c2(ID2_b, gm2_b, W_2, V_GS_2, V_DS_2);
    calculate c3(ID3_b, gm3_b, W_3, V_GS_3, V_DS_3);
    calculate c4(ID4_b, gm4_b, W_4, V_GS_4, V_DS_4);
    calculate c5(ID5_b, gm5_b, W_5, V_GS_5, V_DS_5);

    sort s1(ID0_b, ID1_b, ID2_b, ID3_b, ID4_b, ID5_b,
            gm0_b, gm1_b, gm2_b, gm3_b, gm4_b, gm5_b,
            mode[0],
            sort_0, sort_1, sort_2, sort_3, sort_4, sort_5
        );

    assign nn1 = mode[1] ? sort_0 : sort_3 ;
    assign nn2 = mode[1] ? sort_1 : sort_4 ;
    assign nn3 = mode[1] ? sort_2 : sort_5 ;
    aassign ans = mode[0] ? (3 * nn1 + 4 * nn2 + 5 * nn3) / 3 : 
                          (nn1 + nn2 + nn3) / 3;

    assign out_n = mode[0] ? ans[9:2] : ans[7:0];

endmodule

module calculate(ID, gm, W, V_GS, V_DS);
input [2:0] V_GS;
input [2:0] V_DS;
input [2:0] W;

output [7:0] ID;
output [7:0] gm;

reg [7:0] ID_b;
reg [7:0] gm_b;

wire [2:0] V1;
wire [3:0] V1_2;

assign V1 = V_GS - 3'd1;
assign V1_2 = V1 << 1;

    always@(*)begin
        if (V1 > V_DS)begin
            ID_b = V_DS * (V1_2 - V_DS);
            gm_b = V_DS << 1;
        end
        else begin
            ID_b = V1 * V1;
            gm_b = V1_2;
        end
    end

    assign ID = ID_b * W / 3;
    assign gm = gm_b * W / 3;
    
endmodule

module sort(
    unsortID_0, unsortID_1, unsortID_2, unsortID_3, unsortID_4, unsortID_5,
    unsortgm_0, unsortgm_1, unsortgm_2, unsortgm_3, unsortgm_4, unsortgm_5,
    mode,
    sort_0, sort_1, sort_2, sort_3, sort_4, sort_5
    );

input [7:0] unsortID_0, unsortID_1, unsortID_2, unsortID_3, unsortID_4, unsortID_5;
input [7:0] unsortgm_0, unsortgm_1, unsortgm_2, unsortgm_3, unsortgm_4, unsortgm_5;
input mode;
output reg [7:0] sort_0, sort_1, sort_2, sort_3, sort_4, sort_5;
    
    reg [7:0] layer1_0, layer1_1, layer1_2, layer1_3, layer1_4, layer1_5;
    reg [7:0] layer2_1, layer2_2, layer2_3, layer2_4;
    reg [7:0] layer3_0, layer3_2, layer3_3, layer3_5;
    reg [7:0] layer4_0, layer4_1, layer4_2, layer4_3, layer4_4, layer4_5;

    // layer 1
    always@(*)begin
        if(mode)begin
            if (unsortID_0 >= unsortID_5)begin
                layer1_0 = unsortID_0;
                layer1_5 = unsortID_5;
            end
            else begin
                layer1_0 = unsortID_5;
                layer1_5 = unsortID_0;
            end
            if (unsortID_1 >= unsortID_3)begin
                layer1_1 = unsortID_1;
                layer1_3 = unsortID_3;
            end
            else begin
                layer1_1 = unsortID_3;
                layer1_3 = unsortID_1;
            end
            if (unsortID_2 >= unsortID_4)begin
                layer1_2 = unsortID_2;
                layer1_4 = unsortID_4;
            end
            else begin
                layer1_2 = unsortID_4;
                layer1_4 = unsortID_2;
            end
        end
        else begin
            if (unsortgm_0 >= unsortgm_5)begin
                layer1_0 = unsortgm_0;
                layer1_5 = unsortgm_5;
            end
            else begin
                layer1_0 = unsortgm_5;
                layer1_5 = unsortgm_0;
            end
            if (unsortgm_1 >= unsortgm_3)begin
                layer1_1 = unsortgm_1;
                layer1_3 = unsortgm_3;
            end
            else begin
                layer1_1 = unsortgm_3;
                layer1_3 = unsortgm_1;
            end
            if (unsortgm_2 >= unsortgm_4)begin
                layer1_2 = unsortgm_2;
                layer1_4 = unsortgm_4;
            end
            else begin
                layer1_2 = unsortgm_4;
                layer1_4 = unsortgm_2;
            end
        end
    end

    // layer 2
    always@(*)begin
        if (layer1_1 >= layer1_2)begin
            layer2_1 = layer1_1;
            layer2_2 = layer1_2;
        end
        else begin
            layer2_1 = layer1_2;
            layer2_2 = layer1_1;
        end
        if (layer1_3 >= layer1_4)begin
            layer2_3 = layer1_3;
            layer2_4 = layer1_4;
        end
        else begin
            layer2_3 = layer1_4;
            layer2_4 = layer1_3;
        end
    end

    // layer 3
    always@(*)begin
        if (layer1_0 >= layer2_3)begin
            layer3_0 = layer1_0;
            layer3_3 = layer2_3;
        end
        else begin
            layer3_0 = layer2_3;
            layer3_3 = layer1_0;
        end
        if (layer2_2 >= layer1_5)begin
            layer3_2 = layer2_2;
            layer3_5 = layer1_5;
        end
        else begin
            layer3_2 = layer1_5;
            layer3_5 = layer2_2;
        end
    end

    // layer 4
    always@(*)begin
        if (layer3_0 >= layer2_1)begin
            layer4_0 = layer3_0;
            layer4_1 = layer2_1;
        end
        else begin
            layer4_0 = layer2_1;
            layer4_1 = layer3_0;
        end
        if (layer3_2 >= layer3_3)begin
            layer4_2 = layer3_2;
            layer4_3 = layer3_3;
        end
        else begin
            layer4_2 = layer3_3;
            layer4_3 = layer3_2;
        end
        if (layer2_4 >= layer3_5)begin
            layer4_4 = layer2_4;
            layer4_5 = layer3_5;
        end
        else begin
            layer4_4 = layer3_5;
            layer4_5 = layer2_4;
        end
    end

    // layer 5
    always@(*)begin
        sort_0 = layer4_0;
        if (layer4_1 >= layer4_2)begin
            sort_1 = layer4_1;
            sort_2 = layer4_2;
        end
        else begin
            sort_1 = layer4_2;
            sort_2 = layer4_1;
        end
        if (layer4_3 >= layer4_4)begin
            sort_3 = layer4_3;
            sort_4 = layer4_4;
        end
        else begin
            sort_3 = layer4_4;
            sort_4 = layer4_3;
        end
        sort_5 = layer4_5;
    end

endmodule