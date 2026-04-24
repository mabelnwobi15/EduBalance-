document.addEventListener("DOMContentLoaded", function () {
    const html = document.documentElement;
    const toggleBtn = document.getElementById("themeToggle");

    const savedTheme = localStorage.getItem("edubalance-theme");
    if (savedTheme) {
        html.setAttribute("data-theme", savedTheme);
    }

    updateButtonText();

    if (toggleBtn) {
        toggleBtn.addEventListener("click", function () {
            const currentTheme = html.getAttribute("data-theme") === "dark" ? "dark" : "light";
            const newTheme = currentTheme === "dark" ? "light" : "dark";

            html.setAttribute("data-theme", newTheme);
            localStorage.setItem("edubalance-theme", newTheme);
            updateButtonText();
        });
    }

    function updateButtonText() {
        if (!toggleBtn) return;
        const currentTheme = html.getAttribute("data-theme");
        toggleBtn.textContent = currentTheme === "dark" ? "☀️ Light Mode" : "🌙 Dark Mode";
    }
});