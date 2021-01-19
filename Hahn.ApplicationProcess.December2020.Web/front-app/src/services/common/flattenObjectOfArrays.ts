const flattenObjOfArr = (obj: Object): Array<string> => {
  let arr = new Array<string>();
  for (let key in obj) {
    obj[key].forEach((errorMsg: string) => {
      arr.push(errorMsg);
    });
  }
  return arr;
};

export default flattenObjOfArr;
