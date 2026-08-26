document.addEventListener("DOMContentLoaded", () => {
    const navbar = document.getElementById("mainNavbar");
    const backToTop = document.getElementById("backToTop");

    const updateScrollState = () => {
        navbar?.classList.toggle("scrolled", window.scrollY > 24);
        backToTop?.classList.toggle("show", window.scrollY > 420);
    };

    updateScrollState();
    window.addEventListener("scroll", updateScrollState, { passive: true });

    backToTop?.addEventListener("click", () => {
        window.scrollTo({ top: 0, behavior: "smooth" });
    });

    const path = window.location.pathname.toLowerCase();
    document.querySelectorAll("[data-nav-controller]").forEach(link => {
        const controller = link.dataset.navController.toLowerCase();
        const isHome = controller === "home" && (path === "/" || path === "/home" || path === "/home/index");
        const isCurrent = isHome || path.startsWith(`/${controller}`);
        link.classList.toggle("active", isCurrent);
        if (isCurrent) link.setAttribute("aria-current", "page");
    });

    const revealItems = document.querySelectorAll(".reveal, .sobre-argentina, .explore-section .explore-card, .destination-card, .quiz-card, .receita-card, .clube-card, .admin-card");
    if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver((entries, obs) => {
            entries.forEach(entry => {
                if (!entry.isIntersecting) return;
                entry.target.classList.add("is-visible");
                obs.unobserve(entry.target);
            });
        }, { threshold: 0.08, rootMargin: "0px 0px -35px" });

        revealItems.forEach((element, index) => {
            element.classList.add("reveal");
            element.style.transitionDelay = `${Math.min(index % 6, 5) * 45}ms`;
            observer.observe(element);
        });
    } else {
        revealItems.forEach(element => element.classList.add("is-visible"));
    }

    document.querySelectorAll(".navbar-collapse .nav-link, .navbar-collapse .btn").forEach(link => {
        link.addEventListener("click", () => {
            const menu = document.getElementById("menu");
            if (menu?.classList.contains("show") && window.bootstrap) {
                bootstrap.Collapse.getOrCreateInstance(menu).hide();
            }
        });
    });

    document.querySelectorAll("form[method='post']").forEach(form => {
        form.addEventListener("submit", () => {
            const submit = form.querySelector("button[type='submit'], input[type='submit']");
            if (!submit || submit.dataset.allowRepeat === "true") return;
            setTimeout(() => {
                submit.disabled = true;
                if (submit.tagName === "BUTTON") submit.textContent = "Enviando...";
            }, 0);
        });
    });
});
