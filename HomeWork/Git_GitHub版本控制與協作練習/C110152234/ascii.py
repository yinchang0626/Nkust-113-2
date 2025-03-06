for times in range (3):
    wordl = sorted(input(" "))
    w = 0
    
    for j in wordl:
        i = 97
        wascii =int(ord(j))
        long = wordl.count(j)
        
        for i in range(i,123):
            if(i != w):
                if (wascii == i):
                    print(chr(i))
                    w = i 
                    for p in range(0,long):
                        print("*",end="" )
                    print('\n')
                
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
                
