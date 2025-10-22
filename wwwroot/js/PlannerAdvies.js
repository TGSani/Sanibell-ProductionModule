document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const tableBody = document.getElementById('PlannerTableBody');

    // Search functionality
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

    // Toggle all selections
    document.getElementById('toggleAll').addEventListener('click', function () {
        const checkboxes = document.querySelectorAll('.selection-item');
        const allChecked = Array.from(checkboxes).every(cb => cb.checked);
        checkboxes.forEach(cb => cb.checked = !allChecked);
        this.textContent = !allChecked ? 'Deselecteer alles' : 'Selecteer alles';
    });

    // show loading on form submit
    document.getElementById('planningForm').addEventListener('submit', function() {
        document.getElementById('loading').style.display = 'block';
    });

    // Auto-hide messages after 5 seconds
    const messages = document.querySelectorAll('.alert, .text-danger');
    messages.forEach(msg => {
        setTimeout(() => {
            msg.style.transition = 'opacity 1s ease-out';
            msg.style.opacity = '0';
            setTimeout(() => msg.remove(), 1000);
        }, 5000); 
    });
});
