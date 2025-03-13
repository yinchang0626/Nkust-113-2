for times in range (3):
    wordl = sorted(input(" "))
    w = 0
    

                
    for a in wordl:
        I = 65
        wascii =int(ord(a))
        long = wordl.count(a)
        
        
        
        for I in range(I,91):
            if(I != w):
                if (wascii == I):
                    print(chr(I))
                    w = I
                    for p in range(0,long):
                        print("*",end="")
                    print('\n')
                
