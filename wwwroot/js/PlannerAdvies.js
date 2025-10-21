document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const tableBody = document.getElementById('PlannerTableBody');

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
});
