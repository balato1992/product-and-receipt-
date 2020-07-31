import React, { useState } from 'react';

import Grid from '@material-ui/core/Grid';

import { CusTable } from '../items/CusTable';
import { CusDialogList } from '../items/CusDialogList';
import * as Methods from '../../Methods'

export function ReceiptPage() {
    const baseUrl = "ReceiptInfo";

    const [productNames, setProductNames] = useState([]);

    /*
        public class ReceiptDatumWithUid
            public int Uid { get; set; }
        public class ReceiptDatum
            public string Id { get; set; }
            public string Payee { get; set; }
            public string ReceiptDate { get; set; }
            public List<ReceiptItemDatum> Items { get; set; }
    
        public class ReceiptItemDatum
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Number { get; set; }
    
     */
    function getDate() {
        let date = new Date();
        date.setHours(date.getHours() + 8);

        // console.log(date.toISOString());

        return date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
    }

    let columns = [
        { title: '編號', field: 'id', initialEditValue: '-', emptyValue: '' },
        { title: '客戶名稱', field: 'payee', initialEditValue: '-', emptyValue: '' },
        { title: '日期', field: 'receiptDate', initialEditValue: getDate, emptyValue: '' },
        //{ title: '合計', field: 'items', type: 'select' },
    ];
    let getDataCallback = (callback) => {

        let url = baseUrl;

        Methods.cusFetchJson(url,
            (result) => {

                let productNames = [];
                for (let datum of result) {

                    datum.uidForView = Methods.cusGetUidForView();

                    for (let item of datum.items) {
                        item.uidForView = Methods.cusGetUidForView();

                        if (productNames.indexOf(item.productName) === -1) {
                            productNames.push(item.productName);
                        }
                    }
                }

                productNames.sort();
                setProductNames(productNames);
                callback(result);
            });
    };
    let editActions = {
        post: (data, resolve, reject, alway) => {

            Methods.cusFetch(baseUrl, "post", data, resolve, reject, alway);
        },
        patch: (data, resolve, reject, alway) => {

            Methods.cusFetch(baseUrl, "patch", data, resolve, reject, alway);
        },
        delete: (data, resolve, reject, alway) => {

            Methods.cusFetch(baseUrl, "delete", data, resolve, reject, alway);
        },
    };

    return (
        <div>

            <Grid container justify="flex-end">
                <Grid item>
                    <CusDialogList title={'項目列表'} items={productNames}></CusDialogList>
                </Grid>
            </Grid>

            <br/>
            <CusTable columns={columns} getDataCallback={getDataCallback} editActions={editActions}
                usingReceiptDetail={true} usingReceiptDetailProductNames={productNames}></CusTable>
        </div>
    );
}
