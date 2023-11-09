function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
};

function mensaje(title, text, icon, detalles) {
    Swal.fire({
        icon,
        title,
        text,
        footer: '<a href="">' + detalles + '</a>'
    })
};