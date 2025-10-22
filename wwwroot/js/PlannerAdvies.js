document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const tableBody = document.getElementById('PlannerTableBody');
    const form = document.getElementById('planningForm');
    const loadingDiv = document.getElementById('loading');
    const validationDiv = document.getElementById('validationErrors');
    const tempMessages = document.querySelectorAll('.alert');

    // search filter

    searchInput.addEventListener('input', function () {
        const searchTerm = searchInput.value.toLowerCase();
        const rows = tableBody.getElementsByTagName('tr');

        for (let row of rows) {
            const rowText = Array.from(row.cells)
                                 .map(cell => cell.innerText.toLowerCase())
                                 .join(' ');
            row.style.display = rowText.includes(searchTerm) ? '' : 'none';
        }
    });

    // Toggle all selecties
    document.getElementById('toggleAll').addEventListener('click', function () {
        const checkboxes = document.querySelectorAll('.selection-item');
        const allChecked = Array.from(checkboxes).every(cb => cb.checked);
        checkboxes.forEach(cb => cb.checked = !allChecked);
        this.textContent = !allChecked ? 'Deselecteer alles' : 'Selecteer alles';
    });

    // Form submit & loading / validation
    form.addEventListener('submit', function(e) {
        const selected = document.querySelectorAll('.selection-item:checked');

        if (selected.length === 0) {
            e.preventDefault();

            if (validationDiv) {
                validationDiv.textContent = "Selecteer minimaal één order om te verzenden.";
                validationDiv.style.display = 'block';
                validationDiv.style.opacity = '1';

                setTimeout(() => {
                    validationDiv.style.transition = 'opacity 1s ease-out';
                    validationDiv.style.opacity = '0';
                    setTimeout(() => validationDiv.style.display = 'none', 1000);
                }, 5000);
            }
            return; 
        }

        if (loadingDiv) {
            loadingDiv.style.display = 'block';
            loadingDiv.style.opacity = '1';
        }
    });

    // Fade-out voor TempData alerts
    tempMessages.forEach(msg => {
        if (msg.offsetParent !== null) { // alleen zichtbaar
            setTimeout(() => {
                msg.style.transition = 'opacity 1s ease-out';
                msg.style.opacity = '0';
                setTimeout(() => msg.remove(), 1000);
            }, 3000);
        }
    });
});
