# net5 微服务框架

**自己慢慢把常用的一些东西更新到里面**

## 基础框架

> .net5

## 数据库

> postgresql， 修改配置，可连接mysql sql
>
> redis
## 文件夹结构

> ApiGateway：网关
> 
> Services：基础服务
> 
> Shared：公共类

## 技术框架
设计模式
> DDD设计思想（分层架构，聚合根，值对象，领域事件）
> 
> CQRS架构

网关
> Consul:服务注册与发现
> 
> Ocelot:网关

基础
> identity:身份认证框架
> 
> identityServer4:统一认证中心
> 
> AutoMapper:实体映射
> 
> EFCore:实体框架Orm
> 
> 仓储封装
> 
> Dapper:数据访问Orm
> 
> UnitOfWork:事务
> 
> ELK:日志中心
> 
> Serilog:日志格式化
> 
> Swagger:接口文档以及版本管理
> 
> Cap：事件总线
> 
> RabbitMq：mq保证数据不丢失
> 
> 全局异常处理
> 
> api统一返回类型
> 
> AutoFac：扩展依赖注入

运维部署
> docker
> 
> docker-compose

## 版本更新
### 2021-09-13
*整理服务，添加docker编排与日志中心，更新包*
### 2020-11-13
*版本升级到net5*
### 2020-11-12
*整理代码，之前的没有记录，从今天开始记录每一次上传的内容*
