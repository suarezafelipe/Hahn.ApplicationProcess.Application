import { DialogController } from "aurelia-dialog";
import { autoinject } from "aurelia-framework";

@autoinject
export class Dialog {
  title?: string;
  message?: string;
  action?: (args?: any) => {};
  listOfMessages?: Array<string>;

  constructor(private dialogController: DialogController) {
    dialogController.settings.centerHorizontalOnly = true;
  }

  activate(model: any) {
    this.message = model.message;
    this.listOfMessages = model.listOfMessages;
    this.title = model.title;
    this.action = model.action;
  }

  ok(): void {
    if (this.action) {
      this.action();
    }
    this.dialogController.ok();
  }

  cancel(): void {
    this.dialogController.close(false);
  }
}
