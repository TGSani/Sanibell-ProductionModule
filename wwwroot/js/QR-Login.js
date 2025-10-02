    // QR code login functionality
    document.addEventListener("DOMContentLoaded", function () {
        const qrInput = document.getElementById("QRinput");

        // focus on the hidden input field to capture scanner input
        qrInput.focus();

        // listen for input from the QR scanner
        let buffer = "";

        document.addEventListener("keypress", function (e) {
            // if the input field is disabled, ignore input
            if (qrInput.disabled) return;

            // QR scanners typically end with an "Enter" key, so we check for that to submit the form
            if (e.key === "Enter") {
                qrInput.value = buffer;
                buffer = "";
                document.querySelector("form").submit();
            } else {
                buffer += e.key; // Since QR scanner is using keypress to input value, we want all values unless its a Enter keypress
            }
        });
    });