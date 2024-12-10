// Alt menü aç/kapa fonksiyonu
function toggleSubMenu(menuId) {
    // Seçilen alt menüyü bul
    const menu = document.getElementById(menuId);

    // Tüm alt menüleri kapat
    document.querySelectorAll('.sub-menu').forEach(subMenu => {
        if (subMenu !== menu) {
            subMenu.style.display = 'none';
        }
    });

    // Seçilen alt menüyü aç/kapa
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
}
function toggleSubMenu(menuId) {
    const menu = document.getElementById(menuId);
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
}

