export default interface DialogModel {
  title?: string;
  message?: string;
  action?: (args?: any) => {};
  listOfMessages?: Array<string>;
}
