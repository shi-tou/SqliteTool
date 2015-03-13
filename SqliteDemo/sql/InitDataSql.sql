CREATE TABLE [T_User] (
  [ID] INTEGER PRIMARY KEY NOT NULL,
  [UserName] TEXT NOT NULL,
  [Password] TEXT NOT NULL,
  [Name] TEXT NOT NULL,
  [Sex] TEXT NOT NULL DEFAULT '男',
  [CreateTime] Text NOT NULL
);

INSERT INTO [T_User] ([UserName],[Password],[Name],[Sex],[CreateTime]) VALUES ('张三','123','张三','M','2014-05-20');
INSERT INTO [T_User] ([UserName],[Password],[Name],[Sex],[CreateTime]) VALUES ('李四','123','李四','M','2014-05-20');
