document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');

    sidebar.addEventListener('mouseleave', () => {
        sidebar.classList.add('collapsed');
    });

    sidebar.addEventListener('mouseenter', () => {
        sidebar.classList.remove('collapsed');
    });
});
