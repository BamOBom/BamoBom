function Notification_success(msg) {
    toastr.success(msg, 'انجام شد', { "progressBar": true, "closeButton": true, "showMethod": "slideDown", "hideMethod": "slideUp", timeOut: 2000 });

};
function Notification_error(msg) {
    toastr.error(msg, 'خطا', { "progressBar": true, "closeButton": true, "showMethod": "slideDown", "hideMethod": "slideUp", timeOut: 2000 });

};
function Notification_warning(msg) {
    toastr.warning(msg, 'هشدار', { "progressBar": true, "closeButton": true, "showMethod": "slideDown", "hideMethod": "slideUp", timeOut: 2000 });

};
function Notification_info(msg) {
    toastr.info(msg, 'اعلان', { "progressBar": true, "closeButton": true, "showMethod": "slideDown", "hideMethod": "slideUp", timeOut: 2000 });
};
