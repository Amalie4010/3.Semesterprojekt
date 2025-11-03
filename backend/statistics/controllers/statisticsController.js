

export function sendInstructions(req, res){

        const data = req.body;

        const instruction = JSON.stringify({
        type: data.type,
        amount: data.amount,
        speed: data.speed
        });

        res.send(instruction);
}

export const receiveDatabaseInformation = async (req, res) => {

}

export const receiveOrder = async (req, res) => {
    const order = req.body;
    res.send("order received");
    console.log(order);
}