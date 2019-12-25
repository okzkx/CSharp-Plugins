## ZJson

### 我的第3代 Json 解析工具

#### 总体介绍

##### 简介
将**Json字符串**解析成为多个**ZJson对象**
然后可舍弃该 Json字符串
可将 ZJson 对象转变为合适的4种 C#对象类型
同样,这4种 C#对象也能快速转换为 ZJson对象

##### 特点
- **O(n)** 时间解析(不算预处理)
- 面向对象设计思路
- 工业化的模块化设计
- 极致的易用性和性能追求
- C#7.x语法,最简洁的语法实现

##### 对比
相对的另外一种实现Json解析的方法是用时即时解析  
空间换时间,总体来说代价更大,不考虑这种实现方式。

#### 类介绍

##### ZJson 对象类型  
- Number(float)
- string
- List<ZJson>
- Dictionary<string,ZJson>

##### 开放属性
- readonly JsonType Type (readonly) 
- readonly object Value
```
只有两个属性,且都是只读的
这意味着想要修改ZJson的类型为Number或string
只能新实例化ZJson,不过这是很方便的
```

##### 开放接口
- 反序列化
  - ToString()
- 序列化
  - static ZJson Parse(string json)
- 索引接口
  - ZJson this[int index] *as List*
  - ZJson this[string key] *as Dictionary*
- ZJson与常用类型的相互转化
  - implicit operator ZJson(T arg)
  - implicit operator T(ZJson arg)


#### 未来版本
1. Json 格式校验
2. Json 对象运算
3. Json 压缩加密
4. Json 直接转为类