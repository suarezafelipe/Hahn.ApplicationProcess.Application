import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;
    config.options.root = "/";
    config.map([
      {
        route: "",
        moduleId: PLATFORM.moduleName("pages/applicant/applicant-form"),
        name: "applicant-form",
      },
      {
        route: "confirmation",
        moduleId: PLATFORM.moduleName("pages/confirmation/confirmation"),
        name: "confirmation",
      },
    ]);

    this.router = router;
  }
}
