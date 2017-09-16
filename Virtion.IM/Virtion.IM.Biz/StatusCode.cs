namespace Virtion.IM.View
{
    public enum StatusCode
    {
        CodeWaitingCallback = -1, // 异步调用成功，请等待回调
        CodeOK = 0, // 操作成功
        CodeSystemBusy = 1, // 系统忙，正在处理中
        CodeNotLoginYet = 2, // 还未登陆
        CodeCreateFileFailed = 3, // 创建文件失败
        CodeTargetIsSelf = 4, // 目标是自己(已废除)
        CodeReloginOK = 5, // 重登陆成功
        CodeOfflineLoginOK = 6, // 离线登陆模式成功
        CodeTimeout = 300, // 超时
        CodeVerifyFailed = 400, // 验证失败
        CodeNoPermission = 401, // 没有权限
        CodeRepeatOper = 402, // 重复操作
        CodeGroupNotFound = 403, // 群组未找到
        CodeUserNotFound = 404, // 用户未找到
        CodeLoginFailed = 500, // 登录失败(此命名有误，应为操作失败)
        CodeForceLogout = 600, // 强制登出(被其他设备同一账号踢下线).
        CodeNetworkDisConnected = 700, // 网络连接断开
        CodeRoomNotExist = 33, // 聊天室不存在
        CodeRoomIsFull = 34, // 聊天室人数已满
        CodeNotInRoom = 35, // 不在聊天室内
        CodeForbidden = 36, // 操作禁止
        CodeAlreadyInRoom = 39, // 已经在聊天室中
        CodeUserNotExist = 804, // 用户不存在
        CodeRequestMicFailed = 806, // 请求Mic失败
        CodeVoiceTimeOver = 807, // 录音时间超出
        CodeRecorderBusy = 808, // 录音设备正在使用
        CodeVoiceTooShort = 809, // 录音时间过短 <1000ms

        CodeInvalidArgument = 1000, // 有不合法参数
        CodeServerProcessError = 1001, // 服务器处理失败(可能含有非法参数)
        CodeDBError = 1002, // 操作数据库失败.
        CodeUnkonwnError = 1100, // 未知错误
    }

}
