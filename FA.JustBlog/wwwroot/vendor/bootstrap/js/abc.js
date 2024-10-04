document.addEventListener('DOMContentLoaded', function () {
    // Select the dropdown toggle element
    var dropdownToggleEl = document.querySelector('#dropdownMenuLink');

    // Initialize the dropdown
    var dropdown = new bootstrap.Dropdown(dropdownToggleEl);

    // Add event listener for the toggle button
    dropdownToggleEl.addEventListener('click', function (event) {
        event.preventDefault();

        // Toggle the dropdown
        if (dropdownToggleEl.getAttribute('aria-expanded') === 'true') {
            dropdown.hide();  // Hide dropdown if it's open
        } else {
            dropdown.show();  // Show dropdown if it's closed
        }
    });
});

function showModal() {
    var modalElement = document.getElementById('exampleModal');
    var modal = new bootstrap.Modal(modalElement);
    modal.show();
}