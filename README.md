# BasicAdapterExample
Basit bir Adapter ile farklı iki interface'e sahip 2 prop'u interaction sistemine bağladığımız bir FPS prototipini içeren repo'dur<br>

Bu Repo daha önce geliştirilmiş olan FPS Prototipinin güncellenmiş versiyonudur. Bu Readme içerisinde sadece yeni eklenen sınıflar ve mevcut sınıflar içerisinde güncellenen kısımlar açıklanacaktır.<br> Bir önceki versiyondaki tüm sınıfların açıklamasına ulaşmak için => https://github.com/Unitarian2/ObserverPattern-MVP-Based-User-Interface <br><br>

---Special Magic Circle---<br>
Bu oyun içerisindeki diğer Magic Circle sınıflarından çalışma şekli olarak bazı farklılıklar gösteren özel bir alan türüdür. Bu örnekte Adapter kullanarak bu özel alan türünü Interaction sistemine implemente etmekteyiz.<br>
<b>IDistanceBasedCircle.cs =></b> Special Magic Circle'ların implemente ettiği interface'dir. ICircle interface'inden farklı olarak GetCalculatedAmountByPos methodu bir parametre almaktadır.<br>
<b>DamageCircleDB.cs =></b> Special Magic Circle Gameobject'ine eklediğimiz Monobehaviour sınıfıdır. Mesafe bazlı bir hasar sistemi hesaplar ve onun değerini döndürür.<br><br>

---Adapter Classes---<br>
<b>SpecialToRegularCircleAdapter.cs =></b> Bu adapter ile IDistanceBasedCircle interface'inin muadili olan GetCalculatedAmount methodunu ICircle tipine dönüştürüyoruz.<br><br>

---<b>ReceiveInteraction.cs Changes</b>---<br>
- Eğer player collider IDistanceBasedCircle tipinde bir alana temas ettiyse, SpecialToRegularCircleAdapter'i kullanarak ICircle tipine dönüştürüyoruz. Bu sayede NotifyObservers methodunda herhangi bir güncelleme yapmadan veya yeni bir method oluşturmadan event'i fire edebiliyoruz.<br>
