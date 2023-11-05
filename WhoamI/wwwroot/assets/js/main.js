var stack_topleft = { "dir1": "down", "dir2": "right", "push": "top" };
var stack_bottomleft = { "dir1": "right", "dir2": "up", "push": "top" };
var stack_modal = { "dir1": "down", "dir2": "right", "push": "top", "modal": true, "overlay_close": true };
var stack_bar_top = { "dir1": "down", "dir2": "right", "push": "top", "spacing1": 0, "spacing2": 0 };
var stack_bar_bottom = { "dir1": "up", "dir2": "right", "spacing1": 0, "spacing2": 0 };
var stack_context = { "dir1": "down", "dir2": "left", "context": $("#stack-context") };

toastr.options = {
    "closeButton": true,
    "debug": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "showDuration": "1000",
    "hideDuration": "1000",
    "timeOut": "10000",
    "extendedTimeOut": "10000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}
/*CLASS
stack: 'stack-topleft',
stack: 'stack-bottomleft',
stack: 'stack-bottomright',
stack: 'stack-modal',
 */

function Alert_default(title, msg, type, btn) {
    if (btn === null) { btn = true; }
    swal({
        title: title,
        text: msg,
        type: type,
        showConfirmButton: btn,
        confirmButtonText: 'Tamam',
        html: true
    });
}
function Alert_Success(title, msg, confirmText) {
    swal({
        title: title,
        text: msg,
        type: 'success',
        showConfirmButton: true,
        confirmButtonText: confirmText,
        html: true
    });
}
function Alert_Warning(title, msg, confirmText) {
    swal({
        title: title,
        text: msg,
        type: 'warning',
        showConfirmButton: true,
        confirmButtonText: confirmText,
        html: true
    });
}
function Alert_Error(title, msg, confirmText) {
    swal({
        title: title,
        text: msg,
        type: 'error',
        showConfirmButton: true,
        confirmButtonText: confirmText,
        html: true
    });
}
function Alert_Info(title, msg, confirmText) {
    swal({
        title: title,
        text: msg,
        type: 'info',
        showConfirmButton: true,
        confirmButtonText: confirmText,
        html: true
    });
}
//swal.close();

function Alert_Time(title, msg, time) {
    swal({
        title: title,
        text: msg,
        timer: time,
        type: "info",
        showConfirmButton: false,
        html: true
        // allowOutsideClick: false
    }, function () {
        window.location.reload();
    });
}
function LoadinModal(title, msg) {
    swal({
        title: title,
        text: msg,
        imageUrl: './Content/assets/uyari/Loading.gif',
        showConfirmButton: false,
        html: true
    });
}
function LoadinModalClose() {
    swal.close();
}

function AlertModal(nerede, txt, cls) {
    var strAlert = "";
    strAlert += '<div class="alert alert-' + cls + ' alert-dismissible fade in" role="alert" >';
    strAlert += '     <button type="button" class="close" data-dismiss="alert" aria-label="Close" >';
    strAlert += '        <span aria-hidden="true">&times;</span>';
    strAlert += '    </button> ';
    strAlert += txt;
    strAlert += '</div>';
    $('#' + nerede).append(strAlert);
    var timeOut = setInterval(function () {
        $('.alert').fadeOut();
        clearInterval(timeOut);
    }, 3000)
}

//---------------------------------------------------------------------

function MsgBox_Success(title = "BAÞARILI", txt) {
    toastr['success'](txt, title)
}

function MsgBox_Warning(title = "UYARI", txt) {
    toastr['warning'](txt, title)
}

function MsgBox_Error(title = "HATA", txt) {
    toastr['error'](txt, title)
}

function MsgBox_info(title = "BÝLGÝ", txt) {
    toastr['info'](txt, title);
}

//---------------------------------------------------------------------

/**
 *  return full.veri.myDate().split(' ')[0]; -> sadece tarih
 *  return full.veri.myDate().split(' ')[1]; -> sadece saat
 *  return full.veri.myDate(); -> tarih ve saat
 * @myDate
 */
String.prototype.myDate = function () {
    var gtry = parseInt(this.replaceAll('/', '').replace('Date', '').replace('(', '').replace(')', ''));
    var tarih = new Date(gtry);
    return tarih.toLocaleString();
}

$(`.modal`).on('show.bs.modal', function (e) {
    console.log('Modal Open -> ', e);
    $('body').addClass('overflow-hidden')
})

$(`.modal`).on('hide.bs.modal', function (e) {
    console.log('Modal Close -> ', e);
    $('body').removeClass('overflow-hidden');
})