//Bu Fonksiyon ajax işlemlerini uzun uzun yapmak yerine hızlı yapmamıza Olanak Tanıyor

function ajaxSpeak(url, data, method, callback) {
    if (url == "" || url == 'undefined') {
        url = location.href;
    }
    if (method == "" || method == 'undefined') {
        method = "post";
    }
    $.ajax({
        method: method, // gönderme tipi
        url: url, // gönderdiğimiz Adres
        data: data, // gönderilcek veriler
        success: function (cevap) { // eğer başarılı ise gelen cevabı cevap değişkenine basar
            //cevap = $.parseJSON(cevap);// Gelen Değeri Json Olarak Parse Ediyoruz
            console.log(cevap);
            
            if (cevap.message != "" && cevap.message != 'undefined') {
                swal(cevap.message, "", cevap.status).then((value) => {
                    speakLoad(cevap);
                    if (callback != "" && callback != 'undefined') {
                        callback(cevap);
                    }

                });
            } else {
                speakLoad(cevap);
            }
            return false;
        }
    });
}

function speakLoad(cevap) {
    if (cevap.href != 'undefined' && cevap.href != '') {
        location.href = cevap.href;
    }
    if (cevap.reload != 'undefined' && cevap.reload != '') {
        location.reload();
    }
}