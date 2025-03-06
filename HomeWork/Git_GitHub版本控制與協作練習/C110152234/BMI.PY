h = int(input('請輸入身高(公分)'))
w = int(input('請輸入體重(公斤)'))

h = float(h/100)
bmi = w / (h*h)
print(bmi)