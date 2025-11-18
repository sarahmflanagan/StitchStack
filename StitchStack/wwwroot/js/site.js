// Site-wide JavaScript

// Show confirmation before deleting items
document.querySelectorAll('a[onclick*="confirm"]').forEach(link => {
    link.addEventListener('click', function (e) {
        if (!confirm('Are you sure you want to delete this item?')) {
            e.preventDefault();
        }
    });
});

// Add active class to current nav link
const currentLocation = location.pathname;
const menuItems = document.querySelectorAll('.navbar-nav .nav-link');
menuItems.forEach(item => {
    if (item.getAttribute('href') === currentLocation) {
        item.classList.add('active');
    }
});
