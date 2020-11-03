import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
declare var $: any;
// declare var google: any;

@Component({
    selector: 'app-counter-component',
    templateUrl: './hoadon.component.html'
})
export class HoaDonComponent {
    size: number = 5;
    dateFrom: any;
    dateTo: any;
    orderID: any;
    order: any = {
        data:[],
        totalRecord:0,
        page:0,
        size:5,
        totalPages:0
    }

    orderDetails: any = {
        data: []
    }

    constructor(
        private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }


    ngOnInit() {
    }

    getHoaDon(cPage) {
        let x = {
            dateFrom: this.dateFrom,
            dateTo: this.dateTo,
            page: cPage,
            size: this.size,

        }
        this.http.post('https://localhost:44377' + '/api/Orders/SP-Danh-Sach-DH-De-OT-Linq', x).subscribe(result => {
            this.order = result;
            this.order = this.order.data;
            console.log(this.order);
            // this.drawPieChart(this.order.data);
        }, error => console.error(error));
    }

    openModal(index)
    {
        this.XemChiTiet(index);
        $('#modalDetail').modal("show");
    }

    XemChiTiet(id) {
        let x = {
            orderId: id,
        }
        this.orderID = id;
        this.http.post('https://localhost:44377' + '/api/Orders/SP-Chi-Tiet-Don-Hang-De2-Cau1-Linq', x).subscribe(result => {
            this.orderDetails = result;
            this.orderDetails = this.orderDetails.data;
            //$('#modalDetail').modal('show');
            console.log(id);
            console.log(this.orderDetails);
            // this.drawPieChart(this.order.data);
            
        }, error => console.error(error));
    }
    // drawPieChart(dataChart) {

    //     var arrData = [["OrderDate", "So Luong", { role: "style" }]];
    //     dataChart.forEach(element => {
    //         let item = [];
    //         item.push(element.orderDate.toString());
    //         item.push(element.soLuong);
    //         item.push('#b87333');
    //         arrData.push(item);
    //     });
    //     var data = google.visualization.arrayToDataTable(arrData);

    //     var view = new google.visualization.DataView(data);
    //     view.setColumns([0, 1,
    //         {
    //             calc: "stringify",
    //             sourceColumn: 1,
    //             type: "string",
    //             role: "annotation"
    //         },
    //         2]);

    //     var options = {
    //         title: "Biểu đồ thu nhập của shipper",
    //         width: 600,
    //         height: 400,
    //         bar: { groupWidth: "95%" },
    //         legend: { position: "none" },
    //     };
    //     var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
    //     chart.draw(view, options);
    // }

}
