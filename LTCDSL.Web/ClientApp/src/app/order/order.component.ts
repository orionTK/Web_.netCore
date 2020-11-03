import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { partition } from 'rxjs';
declare var google: any;

@Component({
    selector: 'app-counter-component',
    templateUrl: './order.component.html'
})
export class OrderComponent {
    dateFrom: any;
    dateTo: any;
    order: any = {
        data: []
    }

    constructor(
        private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }


    ngOnInit() {

    }

    getOrder() {
        let x = {

            day: 0,
            keyword: "string",
            ten: "string",
            date: "2020-07-02T02:40:33.692Z",
            dateFrom: this.dateFrom,
            dateTo: this.dateTo,
            isQuantity: true,
            page: 0,
            size: 0,
            month: 0,
            year: 0,
            orderId: 0,
            customerId: "string",
            employeeId: 0,
            orderDate: "2020-07-02T02:40:33.693Z",
            requiredDate: "2020-07-02T02:40:33.693Z",
            shippedDate: "2020-07-02T02:40:33.693Z",
            shipVia: 0,
            freight: 0,
            shipName: "string",
            shipAddress: "string",
            shipCity: "string",
            shipRegion: "string",
            shipPostalCode: "string",
            shipCountry: "string"

        }

        this.http.post('https://localhost:44377' + '/api/Orders/SP-So-Luong-SP-Can-Gia', x).subscribe(result => {
            this.order = result;
            console.log(this.order);
            this.drawPieChart(this.order.data);
        }, error => console.error(error));
    }

    drawPieChart(dataChart) {
        
        var arrData = [["OrderDate", "So Luong", { role: "style" }]];
        dataChart.forEach(element => {
            let item = [];
            item.push(element.orderDate.toString());
            item.push(element.soLuong);
            item.push('#b87333');
            arrData.push(item);
        });
        var data = google.visualization.arrayToDataTable(arrData);

        var view = new google.visualization.DataView(data);
        view.setColumns([0, 1,
            {
                calc: "stringify",
                sourceColumn: 1,
                type: "string",
                role: "annotation"
            },
            2]);

        var options = {
            title: "Biểu đồ thu nhập của shipper",
            width: 600,
            height: 400,
            bar: { groupWidth: "95%" },
            legend: { position: "none" },
        };
        var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
        chart.draw(view, options);
    }

}
