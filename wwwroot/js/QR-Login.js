    // QR code login functionaliteit
    document.addEventListener("DOMContentLoaded", function () {
        const qrInput = document.getElementById("QRinput");

        // Focus automatisch op het inputveld (ook al is het verborgen)
        qrInput.focus();

        // Luister naar toetsenbordinput (bijvoorbeeld van een QR-scanner die als keyboard werkt)
        let buffer = "";

        document.addEventListener("keypress", function (e) {
            // Stop als een echte submit al bezig is
            if (qrInput.disabled) return;

            // QR-scanners eindigen vaak met Enter → dan submitten we
            if (e.key === "Enter") {
                qrInput.value = buffer;
                buffer = "";
                document.querySelector("form").submit();
            } else {
                buffer += e.key;
            }
        });
    });