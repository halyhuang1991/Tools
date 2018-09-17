def testFun0():
     temp = [lambda x : i*x for i in range(4)] 
     return temp 
for everyLambda in testFun0(): 
    print (everyLambda(2))

print('--1-----------------')
from functools import partial 
from operator import mul 
def testFun(): 
    return [partial(mul, i) for i in range(4)] 
for everyLambda in testFun(): 
    print(everyLambda(2))
print('--2-----------------')
def testFun1(): 
    temp = [lambda x, i=i: i * x for i in range(4)] 
    return temp 
for everyLambda in testFun1():
     print(everyLambda(2))
print('--3-----------------')
def testFun2(): 
    return (lambda x, i=i: i * x for i in range(4)) 
for everyLambda in testFun2(): 
    print(everyLambda(2))
print('--4-----------------')
def testFun3(): 
    for i in range(4): 
        yield lambda x: i * x 
for everyLambda in testFun3():
     print(everyLambda(2))