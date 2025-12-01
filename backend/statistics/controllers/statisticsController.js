import fs from 'fs';

export const receiveDatabaseInformation = async (req, res) => {

}

export const receiveOrder = async (req, res) => {
    const order = req.body;
    res.send("order received");

    let content = fs.readFileSync('backend/statistics/orders.json');
    let fileArr = [];
    if(content.length > 0){
        fileArr = JSON.parse(content);
    }
    fileArr.push(order);
    content = JSON.stringify(fileArr);
    fs.writeFileSync('backend/statistics/orders.json', content);
}