# 🕹️ SwitchController  
> 使用电脑远程控制 Nintendo Switch，就像真实手柄一样操作，并可实时查看 Switch 屏幕画面。  
<img width="882" height="412" alt="image" src="https://github.com/user-attachments/assets/1c936579-d829-46fc-af9f-02c5400e2b2a" />

<img width="880" height="411" alt="image" src="https://github.com/user-attachments/assets/b9695559-3e4b-46b9-b3c9-5a22fcff1713" />

<img width="883" height="413" alt="image" src="https://github.com/user-attachments/assets/3740edcb-d065-4623-b77d-6d1298e1894b" />

---

## 📖 项目简介
**SwitchController** 是一个基于 Sys-BotBase 的远程控制与自动化工具。  
通过该程序，你可以：
- 使用电脑直接控制 Switch，模拟真实 Joy-Con 操作；
- 实时查看 Switch 屏幕画面；
- 自动化执行任务（如宏操作、连发、按键序列）；
- 将控制与显示整合在一个界面中，方便玩家与开发者测试游戏或脚本。

本项目旨在为 Switch 研究、自动化测试与控制实验提供开放、易用的解决方案。

---

## ⚙️ 主要功能
- 🎮 模拟 Joy-Con 手柄操作（A/B/X/Y、摇杆、触控等）
- 📺 实时显示 Switch 屏幕画面
- 🔁 支持任务脚本（自动化执行）
- 🧩 支持网络连接，控制主机无需实体手柄
- 🧠 可扩展 API，便于自定义按键行为或外部程序控制

---

## 🧱 基础原理
本项目依赖于 **Sys-BotBase**，它是一个运行在已破解 Nintendo Switch 上的后台服务，
允许通过网络接口接收命令（如按键、内存操作等）。

系统架构示意：


> ⚠️ 注意：使用本项目前，Switch 必须运行 `Sys-BotBase`，并确保主机与电脑在同一局域网内。

---

## 🖥️ 使用方法
### 1️⃣ 准备环境
- 一台已安装 **Sys-BotBase** 的 Nintendo Switch；
- 一台与 Switch 在同一网络下的电脑；
- 安装 [.NET 9.0+ Runtime](https://dotnet.microsoft.com/download/dotnet)；
- 下载或编译本项目的可执行文件。

### 2️⃣ 启动 Sys-BotBase
在 Switch 端通过 Homebrew 启动 `Sys-BotBase`，等待显示“listening on port 6000”提示。

### 3️⃣ 连接并控制
1. 打开 SwitchController；
2. 输入 Switch 的 IP 地址；
3. 点击“连接”；
4. 使用键盘、手柄或脚本执行操作；
5. 即可在界面中实时查看 Switch 屏幕画面。

---

## ⚙️ 技术细节
| 模块 | 功能说明 |
|------|-----------|
| **GY.Matlab.Core.WindService** | 任务执行/初始化模块（示例代码参考） |
| **SysBotClient** | 与 Sys-BotBase 通信的核心类 |
| **InputHandler** | 将电脑输入转换为按键指令 |
| **DisplayStream** | 负责视频流/图像显示（可结合 SysDVR） |

---

## 🧩 可选功能
- 脚本模式：支持导入 JSON / CSV 格式任务脚本；
- API 模式：提供本地 TCP 接口，可供外部程序控制；
- 自定义热键绑定；
- 多主机管理（实验性）。

---

## ⚠️ 注意事项 / 声明
- 本项目仅用于 **个人研究、学习与自制软件开发**；
- 请勿用于违反 Nintendo 使用条款或在线服务；
- 使用本项目所导致的任何设备损坏或账号封禁风险由用户自行承担；
- 本项目与 Nintendo 无任何关联。

---

## ⭐ 支持本项目
如果这个项目对你有帮助，欢迎：
- 点亮 Star ⭐ 支持；
- 提交 issue/PR 改进功能；
- 分享给其他需要远程控制 Switch 的开发者。

---

> “让控制与自由结合，像真正的手柄一样体验 Switch。”


## 🪪 License / 许可声明

本项目 **SwitchController** 基于 [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.html) 开源发布。

你可以：
- ✅ 自由使用、修改、分发本项目的代码；
- ✅ 在遵守 GPLv3 的前提下，将其用于研究、教学、或其他非商业/商业用途；
- ⚠️ 但必须在发布衍生作品时 **保持相同许可证（GPLv3）** 并保留版权与声明。

---

### 🔗 包含的上游项目
本项目基于以下开源项目开发：

| 依赖项目 | 作者 | 协议 |
|-----------|------|------|
| [Sys-BotBase](https://github.com/olliz0r/sys-botbase) | olliz0r | GPL-3.0 |
| [SysBot.Base](https://github.com/kwsch/SysBot.NET) | kwsch | MIT |
| SwitchController | ZiYuKing | GPL-3.0 |

由于本项目直接依赖并与 Sys-BotBase (GPLv3) 链接，  
因此本项目整体遵循 GPLv3 协议。

📄 **完整许可证文本见：[LICENSE](LICENSE)**

