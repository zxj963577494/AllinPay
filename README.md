# 通联支付 - 小通分期 - C#

## 环境

* .NET Framework 4.0
* Visual Studio 2013

## 快速开始

* 将公钥、私钥文件放入项目key文件夹下(根据配置)
* 打开AllinPayConfig.cs文件进行配置
* 打开Default.aspx文件进行配置

## IIS部署

iis 找到部署的站点应用连接池，右键高级设置，找到“加载用户配置文件”改为true。window service2008 默认为false的。

* [X509Certificate2 本地正常，放到线上内部错误](http://bbs.csdn.net/topics/390901767)
* [一桩由X509Certificate2引发的血案](http://www.cnblogs.com/uptothesky/p/5972124.html)

