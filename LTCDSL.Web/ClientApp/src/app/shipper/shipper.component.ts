import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { partition } from 'rxjs';
declare var google: any;

@Component({
    selector: 'app-counter-component',
    templateUrl: './shipper.component.html'
})
export class ShipperComponent {
    month: any;
    year: any;
    shipper: any = {
        data: []
    }

    constructor(
        private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }


    ngOnInit() {

    }

    getShipper() {
        let x = {
            month: parseInt(this.month),
            year: parseInt(this.year),
            shipperId: 0,
            companyName: "string",
            phone: "string"
        }

        this.http.post('https://localhost:44377' + '/api/Shippers/SP-Danh-Sach-Shipper-OT-Linq', x).subscribe(result => {
            this.shipper = result;
            console.log(this.shipper);
            this.drawPieChart(this.shipper.data);
        }, error => console.error(error));
    }

    drawPieChart(dataChart) {
        var arrData = [["CompanyName", "Tien", { role: "style" }]];
        dataChart.forEach(element => {
            let item = [];
            item.push(element.companyName);
            item.push(parseFloat(element.tien));
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
            title: "Biểu đồ số lượng hàng hóa",
            width: 600,
            height: 400,
            bar: { groupWidth: "95%" },
            legend: { position: "none" },
        };
        var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
        chart.draw(view, options);
    }

}
