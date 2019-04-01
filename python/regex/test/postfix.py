# 中缀表达式
infix_expression = '9+(3-1)*3+10/2'
 
# 优先级列表
priority1 = ['*', '/']
priority2 = ['+', '-']
# 字符是否使用列表
useable = [True for i in range(len(infix_expression))]
# 初始化堆栈和后缀表达式
stack = []
postfix_expression = []
for i in range(len(infix_expression)):
    # 防止操作数字符重复使用
    if useable[i] is True:
        # 操作数字符直接加入堆栈
        if infix_expression[i].isdigit():
            operand = [infix_expression[i]]
            if i < len(infix_expression) - 1:
                j = 1
                # 将长度大于1的操作数作为整体加入堆栈
                while (infix_expression[i + j].isdigit() or infix_expression[i + j] is '.'):
                    operand.append(infix_expression[i + j])
                    useable[i + j] = False
                    j += 1
                    if (i + j) == len(infix_expression):
                        break
            operand = "".join(operand)
            postfix_expression.append(operand)
        else:
            # 堆栈列表不为空
            if len(stack):
                # '('直接加入堆栈
                if infix_expression[i] is '(':
                    stack.append(infix_expression[i])
                # 处理处于优先级1的运算符
                elif infix_expression[i] in priority1:
                    while(True):
                        if len(stack) == 0 or stack[-1] in priority2 or stack[-1] is '(':
                            stack.append(infix_expression[i])
                            break
                        else:
                            postfix_expression.append(stack.pop())
                # 处理处于优先级2的运算符
                elif infix_expression[i] in priority2:
                    while (True):
                        if len(stack) == 0 or stack[-1] is '(':
                            stack.append(infix_expression[i])
                            break
                        else:
                            postfix_expression.append(stack.pop())
                # 处理')'
                elif infix_expression[i] is ')':
                    while(stack[-1] != '('):
                        postfix_expression.append(stack.pop())
                    stack.pop()
            # 堆栈列表为空，直接加入堆栈
            else:
                stack.append(infix_expression[i])
# 获得最终的后缀表达式
for i in range(len(stack)):
    postfix_expression.append(stack.pop())
# 利用后缀表达式计算结果
for i in range(len(postfix_expression)):
    if postfix_expression[i].isdigit():
        stack.append(postfix_expression[i])
    else:
        oper1 = float(stack.pop())
        oper2 = float(stack.pop())
        if postfix_expression[i] == '+':
            stack.append(oper2 + oper1)
        elif postfix_expression[i] == '-':
            stack.append(oper2 - oper1)
        elif postfix_expression[i] == '*':
            stack.append(oper2 * oper1)
        elif postfix_expression[i] == '/':
            stack.append(oper2 / oper1)
print('中缀表达式;', infix_expression)
print('后缀表达式:', " ".join(postfix_expression))
print('计算结果:', stack[0])