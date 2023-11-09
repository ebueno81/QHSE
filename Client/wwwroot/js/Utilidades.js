function DescargarArchivo(nombre, base64) {

    const link = document.createElement("a");
    link.download = nombre;
  /*  link.href = "data:application/octet-stream;base64," + base64;*/
    /*link.href = "data:application/vnd.ms-excel;base64," + base64;*/
    link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + base64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
};

