import fs from 'fs';

let orderList = "";

export function sendInstructions(req, res){
        const data = req.body;
        data.foreach((element) => res.send(element));
        console.log(res.body);
}

export const receiveDatabaseInformation = async (req, res) => {

}

export const receiveOrder = async (req, res) => {
    const order = req.body;
    res.send("order received");

    let content = fs.readFileSync('backend/statistics/orders.json');
    console.log(content + "file");

    let fileArr = [];

    if(content.length > 0){
        fileArr = JSON.parse(content);
    }

    console.log(fileArr + "fileArr");
    fileArr.push(order);

    content = JSON.stringify(fileArr);

    console.log(content + "file 2");
    fs.writeFileSync('backend/statistics/orders.json', content);
}