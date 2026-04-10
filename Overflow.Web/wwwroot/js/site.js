// Theme toggle logic
(function () {
    'use strict';

    var STORAGE_KEY = 'overflow-theme';
    var toggleBtn = document.getElementById('theme-toggle');
    var themeIcon = document.getElementById('theme-icon');

    function getStoredTheme() {
        return localStorage.getItem(STORAGE_KEY);
    }

    function getPreferredTheme() {
        var stored = getStoredTheme();
        if (stored) return stored;
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function setTheme(theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        document.documentElement.setAttribute('data-theme', theme);
        localStorage.setItem(STORAGE_KEY, theme);
        updateIcon(theme);
    }

    function updateIcon(theme) {
        if (!themeIcon) return;
        themeIcon.textContent = theme === 'dark' ? '☀️' : '🌙';
        if (toggleBtn) {
            toggleBtn.title = theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode';
            toggleBtn.setAttribute('aria-label', theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode');
        }
    }

    // Apply current theme and icon on page load
    var currentTheme = getPreferredTheme();
    updateIcon(currentTheme);

    // Toggle on click
    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            var current = document.documentElement.getAttribute('data-theme') || 'light';
            setTheme(current === 'dark' ? 'light' : 'dark');
        });
    }

    // Listen for OS-level preference changes (only if no user preference stored)
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
        if (!getStoredTheme()) {
            setTheme(e.matches ? 'dark' : 'light');
        }
    });
})();

