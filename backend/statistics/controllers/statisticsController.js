export const sendInstructions = async (req, res) => {};

export const receiveDatabaseInformation = async (req, res) => {};

export const receiveOrder = async (req, res) => {
  const order = req.body;
  res.send(order);
  console.log(order);
};
