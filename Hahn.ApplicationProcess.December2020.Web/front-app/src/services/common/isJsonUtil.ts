const isJson = (str: string): boolean => {
  try {
    JSON.parse(str);
    return true;
  } catch (_) {
    return false;
  }
};

export default isJson;
