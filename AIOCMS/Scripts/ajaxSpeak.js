//Bu Fonksiyon ajax işlemlerini uzun uzun yapmak yerine hızlı yapmamıza Olanak Tanıyor

function ajaxSpeak(url, data, callback, method) {
    if (url == "" || url == "undefined") {
        url = location.href;
    }
    if (method == "" || method == "undefined") {
        method = "post";
    }
    $.ajax({
        method: method, // gönderme tipi
        url: url, // gönderdiğimiz Adres
        data: data, // gönderilcek veriler
        success: function (cevap) { // eğer başarılı ise gelen cevabı cevap değişkenine basar
            cevap = $.parseJSON(cevap);// Gelen Değeri Json Olarak Parse Ediyoruz
            swal(cevap.message, "", cevap.status).then((value) => {
                if (cevap.href != 'undefined') {
                    location.href = cevap.href;
                }
                if (cevap.reload != 'undefined') {
                    location.reload();
                }
                callback(cevap);
            });
            return false;
        }
    });
}