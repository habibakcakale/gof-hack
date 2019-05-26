import Vue from "vue";
import Router from "vue-router";
import routes from "vue-auto-routing";
import store from "@/store/index";
import { createRouterLayout } from "vue-router-layout";

Vue.use(Router);

const RouterLayout = createRouterLayout(layout => {
  return import("@/layouts/" + layout + ".vue");
});

console.log(routes.map(el => el.component));

const router = new Router({
  mode: "history",
  routes: [
    {
      path: "/",
      component: RouterLayout,
      children: [
        {
          name: "index",
          path: "",
          component: () => import("@/pages/index.vue"),
          meta: {
            isPublic: true
          }
        },
        {
          name: "about-product",
          path: "about-product",
          component: () => import("@/pages/about-product.vue"),
          meta: {
            isPublic: true
          }
        },
        {
          name: "login",
          path: "login",
          component: () => import("@/pages/login.vue"),
          meta: {
            isPublic: true,
            alreadyLogged: true
          }
        },
        {
          name: "register",
          path: "register",
          component: () => import("@/pages/register.vue"),
          meta: {
            isPublic: true,
            alreadyLogged: true
          }
        },
        {
          name: "dashboard",
          path: "dashboard",
          component: () => import("@/pages/dashboard.vue")
        },
        {
          name: "project-summary-id",
          path: "project-summary/:id?",
          component: () => import("@/pages/project-summary/_id.vue")
        }
      ]
    }
  ]
});

router.beforeEach((to, from, next) => {
  const hasToken = store.getters.token;
  if (!to.meta.isPublic) {
    if (hasToken) {
      if (to.meta.alreadyLogged) {
        next({
          path: "/dashboard",
          query: {
            redirect: to.fullPath
          }
        });
      }
      return next();
    }
    return next({
      path: "/login",
      query: {
        redirect: to.fullPath
      }
    });
  }

  next();
});

export default router;
