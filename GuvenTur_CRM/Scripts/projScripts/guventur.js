/// <reference path="../jquery-1.10.2.min.js" />

//---------------------------Common Scripts Begin-----------------------------------------------------------------------------------------------------------//

//---------------Get Town List By Id-----------------------------//
$(document).ready(function () {
    $.getTownsById = function () {

        var id = $('#counties').val();
        $.ajax({
            method: 'POST',
            url: '/Common/GetTownList',
            data: { id: id },
            dataType: 'json',
            success: function (towns) {
                if (towns != null) {
                    for (var i = 0; i < towns.length; i++) {
                        $("#towns").append('<option value="' + towns[i].Id + '">' + towns[i].Town_Name + '</option>');
                    }
                }
            },
            error: function () {
                alert("İlçeler Yüklenirken Hata Oluştu");
            }
        });
    }
});
//--------------------------------------------------------------//

//---------------Get Vehicle Plate List By Service Id-----------------------------//
$(document).ready(function () {
    $.getVehiclesInfoById = function (choose) {

        var id = $('#services').val();
        $.ajax({
            method: 'POST',
            url: '/Common/GetVehiclesInfoByServiceId',
            data: { id: id },
            dataType: 'json',
            success: function (vehicle) {
                if (vehicle != null) {
                    if (choose == 'vehicle') {
                        for (var i = 0; i < vehicle.length; i++) {
                            $("#vehicleNo").append('<option value="' + vehicle[i].Id + '">' + vehicle[i].Vehicle_Plate + '</option>');
                        }
                    }
                    if (choose == 'member') {
                        for (var i = 0; i < vehicle.length; i++) {
                            $("#vehicleNo").append('<option value="' + vehicle[i].Id + '">' + vehicle[i].Vehicle_No + '</option>');
                        }
                    }
                }
            },
            error: function () {
                alert("Araç Bilgiler Yüklenirken Hata Oluştu");
            }
        });
    }
});
//--------------------------------------------------------------//

//---------------Get Members List By Member Group Id-----------------------------//
$(document).ready(function () {
    $.getMembersByGroupId = function (choose) {

        var id = $('#memberGroups').val();
        $.ajax({
            method: 'POST',
            url: '/Common/GetMembersByMemGroupId',
            data: { id: id },
            dataType: 'json',
            success: function (members) {
                if (members != null) {
                    for (var i = 0; i < members.length; i++) {
                        $("#members").append('<option value="' + members[i].Id + '">' + members[i].Member_Name + '</option>');
                    }
                }
            },
            error: function () {
                alert("Üye Bilgiler Yüklenirken Hata Oluştu");
            }
        });
    }
});
//--------------------------------------------------------------//

//---------------------------Common Scripts End-----------------------------------------------------------------------------------------------------------//



//--------------------------Company Scripts Begin--------------------------------------------------------------------------------------------------------//

//---------------Add Or Edit Company Function---------------------------------//
$(document).ready(function () {
    $.addOrEditCompany = function (id) {

        var service = {
            id: id,
            serviceName: $('#serviceName').val(),
            countryId: $('#countries').val(),
            countyId: $('#counties').val(),
            townId: $('#towns').val(),
            address: $('#addreses').val(),
            phone: $('#phones').val(),
            fax: $('#fax').val(),
            email: $('#comEmail').val(),
            authName: $('#authName').val(),
            authPhone: $('#authPhone').val(),
            authEmail: $('#authEmail').val(),
            companyTitle: $('#companyTitle').val(),
            taxOffice: $('#taxOffice').val(),
            taxNumber: $('#taxNumber').val()
        };

        $.ajax({
            method: 'POST',
            url: '/Companies/Add_Or_Edit_Company',
            data: service,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='background-color:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Companies/Add_New_Company";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Companies/Add_New_Company";
                }, 5000);
            }
        });
    }
});
//---------------------------------------------------------------------------//

//---------------Delete Company Function------------------------------------//
$(document).ready(function () {
    $.deleteCompany = function (id) {
        var result = confirm('Servisi Silmek İstediğinizden Emin misiniz?');

        if (result) {
            $.ajax({
                method: 'POST',
                url: '/Companies/Delete_Company',
                data: { id: id },
                dataType: 'json',
                success: function (result) {
                    alert(result);
                    location.href = "/Companies/Index";
                },
                error: function (error) {
                    alert(error.message);
                }
            });
        } else {
            return;
        }
    }
});
//-------------------------------------------------------------------------//

//--------------------------Company Scripts End-------------------------------------------------------------------------------------------------------//



//--------------------------Vehicle Scripts Begin-----------------------------------------------------------------------------------------------------------------//

//---------------Add Or Edit Vehicle Function---------------------------//
$(document).ready(function () {

    $.addOrEditVehicle = function (id) {
        var vehicle = {
            id: id,
            driverTc: $('#driverTc').val(),
            driverName: $('#driverName').val(),
            countryId: $('#countries').val(),
            countyId: $('#counties').val(),
            townId: $('#towns').val(),
            address: $('#addreses').val(),
            phone: $('#phones').val(),
            gsm: $('#gsm').val(),
            email: $('#vhcEmail').val(),
            salary: $('#vhcSalary').val(),
            vehicleNo: $('#vhcNo').val(),
            vehiclePlate: $('#vhcPlate').val(),
            serviceId: $('#services').val(),
            firstKmMaintnc: $('#firstKmMain').val(),
            nextKmMaintnc: $('#nextKmMain').val(),
            trrafficInsrncDate: $('#trffcInsuranceDate').val(),
            examinationDate: $('#examiDate').val(),
            licenseSample: $('#fileLicenseSample').val(),
            trafficInsrncSample: $('#fileTrffcInsSample').val(),
            examination: $('#fileExamination').val(),
            driverLicense: $('#fileDrvLicense').val(),
            registryRecord: $('#fileRegistryRecord').val(),
            gbt: $('#fileGbt').val(),
            healthReport: $('#fileHealthReport').val()
        };

        debugger;
        $.ajax({
            method: 'POST',
            url: '/Vehicles/Add_Or_Edit_Vehicle',
            data: vehicle,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='background-color:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Add_New_Vehicles";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Add_New_Vehicles";
                }, 5000);
            }
        });
    }
});
//---------------------------------------------------------------------//

//---------------Delete Vehicle Function------------------------------//
$(document).ready(function () {
    $.deleteVehicle = function (id) {
        var result = confirm('Aracı Silmek İstediğinizden Emin misiniz?');

        if (result) {
            $.ajax({
                method: 'POST',
                url: '/Vehicles/Delete_Vehicle',
                data: { id: id },
                dataType: 'json',
                success: function (result) {
                    alert(result);
                    location.href = "/Vehicles/Vehicles";
                },
                error: function (error) {
                    alert(error.message);
                }
            });
        } else {
            return;
        }
    }
});
//-------------------------------------------------------------------//

//---------------Add Vehicle Maintenance Function-------------------//
$(document).ready(function () {
    $.addVehicleMaintenance = function () {

        var maintenance = {
            vehicleId: $('#plates').val(),
            serviceId: $('#services').val(),
            vehicleKm: $('#vehicleKm').val(),
            maintenanceTypeId: $('#maintenanceType').val(),
            maintenanceDate: $('#maintenanceDate').val(),
            maintenancePayment: $('#payment').val()
        };

        debugger;
        $.ajax({
            method: 'POST',
            url: '/Vehicles/Add_Vehilce_Maintenance',
            data: maintenance,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Add_Maintenance";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Add_Maintenance";
                }, 5000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------Do Vehicle Payment Function----------------------//
$(document).ready(function () {
    $.doVehiclePayment = function () {

        var vhcPayment = {
            paymentDate: $('#paymentDate').val() + ' ' + $('#paymentTime').val(),
            vehicleId: $('#plates').val(),
            paymentTypeId: $('#paymentType').val(),
            payment: $('#payment').val()
        };

        $.ajax({
            method: 'POST',
            url: '/Vehicles/Do_Vehicle_Payment',
            data: vhcPayment,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Vehicle_Payments";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Vehicles/Vehicle_Payments";
                }, 5000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------------------Vehicle Scripts End---------------------------------------------------------------------------------------------------------------//



//--------------------------Member Scripts Begin--------------------------------------------------------------------------------------------------------------//

//---------------Add Or Edit Member Function---------------------------//
$(document).ready(function () {
    $.addOrEditMember = function (id) {

        var member = {
            id: id,
            memberGroupId: $('#memberGroup').val(),
            memberName: $('#memberName').val(),
            serviceId: $('#services').val(),
            vehicleId: $('#vehicleNo').val(),
            countryId: $('#countries').val(),
            countyId: $('#counties').val(),
            townId: $('#towns').val(),
            address: $('#addreses').val(),
            phone: $('#phones').val(),
            gsm: $('#gsm').val(),
            email: $('#memEmail').val(),
            borrow: $('#borrow').val(),
            km: $('#memberKm').val(),
            memberTitle: $('#memberTitle').val(),
            memberTc: $('#memberTc').val(),
            bankName: $('#bankName').val(),
            cardOwner: $('#cardOwner').val(),
            cardNumber: $('#cardNumber').val(),
            lastExpDate: $('#lastExpDate').val(),
            securtyNo: $('#securtyNo').val()
        };

        $.ajax({
            method: 'POST',
            url: '/Members/Add_Or_Edit_Member',
            data: member,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='background-color:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Members/Add_New_Member";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Members/Add_New_Member";
                }, 5000);
            }
        });
    }
});
//---------------------------------------------------------------------//

//---------------Delete Member Function------------------------------//
$(document).ready(function () {
    $.deleteMember = function (id) {
        var result = confirm('Üyeyi Silmek İstediğinizden Emin misiniz?');

        if (result) {
            $.ajax({
                method: 'POST',
                url: '/Members/Delete_Member',
                data: { id: id },
                dataType: 'json',
                success: function (result) {
                    alert(result);
                    $.location.href("/Members/Members");
                },
                error: function (err) {
                    alert(err.message);
                }
            });
        } else {
            return;
        }
    }
});
//-------------------------------------------------------------------//

//---------------Add Member Payment Function----------------------//
$(document).ready(function () {
    $.addMemberPayment = function () {

        var memPayment = {
            memberId: $('#members').val(),
            payment: $('#payment').val(),
            paymentTypeId: $('#paymentType').val(),
            paymentDate: $('#paymentDate').val() + ' ' + $('#paymentTime').val()
        };

        $.ajax({
            method: 'POST',
            url: '/Members/Add_Member_Payment',
            data: memPayment,
            dataType: 'json',
            success: function () {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>Ödeme Kaydı Başarıyla Gerçekleşti.</label></div>");
                setInterval(function () {
                    location.href = "/Members/Member_Payments";
                }, 5000);
            },
            error: function () {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>Ödeme Kaydı Esnasında Bir Hata Oluştu!</label></div>");
                setInterval(function () {
                    location.href = "/Members/Member_Payments";
                }, 5000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------------------Member Scripts End--------------------------------------------------------------------------------------------------------------//



//--------------------------User And Setting Scripts Begin--------------------------------------------------------------------------------------------------------------//

//---------------Add Or Edit User Function---------------------------//
$(document).ready(function () {
    $.addOrEditUSer = function (id) {

        var user = {
            id: id,
            levelId: $('#levels').val(),
            userTitle: $('#userTitle').val(),
            userFirstName: $('#userFirstName').val(),
            userLastName: $('#userLastName').val(),
            password: $('#userPassword').val(),
            phoneLine: $('#phoneLine').val(),
            gsm: $('#gsm').val(),
            email: $('#userEmail').val(),
            address: $('#addreses').val(),
            userTc: $('#userTc').val()
        };

        debugger;
        $.ajax({
            method: 'POST',
            url: '/Settings/Add_Or_Edit_User',
            data: user,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div class='col-md-12' style='background-color:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    location.href = "/Companies/Add_New_Company";
                }, 5000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Companies/Add_New_Company";
                }, 5000);
            }
        });
    }
});
//---------------------------------------------------------------------//

//---------------Delete User Function------------------------------//
$(document).ready(function () {
    $.deleteUser = function (id) {
        var result = confirm('Kullanıcıyı Silmek İstediğinizden Emin misiniz?');

        if (result) {
            $.ajax({
                url: '/Settings/Delete_User',
                data: { id: id },
                method: 'post',
                dataType: '',
                success: function (result) {
                    alert(result);
                    $.location.href("/Settings/Users");
                },
                error: function (err) {
                    alert(err.message);
                }
            });
        } else {
            return;
        }
    }
});
//-------------------------------------------------------------------//

//---------------Add Or Edit Special Settings Function----------------------//
$(document).ready(function () {
    $.addOrEditUserSpecialSettings = function (id) {

        var data = {
            id: id,
            userId: $('#userId').val(),
            addCompany: $('#addCompany').val(),
            editCompany: $('#editCompany').val(),
            deleteCompany: $('#deleteCompany').val(),
            seeCompAccounts: $('#seeCompAccounts').val(),
            addVehicle: $('#addVehicle').val(),
            editVehicle: $('#editVehicle').val(),
            deleteVehicle: $('#deleteVehicle').val(),
            seeVehicleAccounts: $('#seeVehicleAccounts').val(),
            addMember: $('#addMember').val(),
            editMember: $('#editMember').val(),
            deleteMember: $('#deleteMember').val(),
            seeMemberAccounts: $('#seeMemberAccounts').val()
        };

        $.ajax({
            url: '/GuvenTurService.asmx/AddOrEditUserSpecialSettings',
            data: data,
            method: 'post',
            dataType: '',
            success: function () {
                //$('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>Ödeme Kaydı Başarıyla Gerçekleşti.</label></div>");
                //setInterval(function () {
                //    location.href = "/Members/Member_Payments";
                //}, 5000);
            },
            error: function () {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>Yetki Kaydı Esnasında Bir Hata Oluştu!</label></div>");
                //setInterval(function () {
                //    location.href = "/Members/Member_Payments";
                //}, 5000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------Add New Level Function----------------------//
$(document).ready(function () {
    $.addNewLevel = function () {

        var level = {
            levelName: $('#levelName').val(),
        };
        debugger;
        $.ajax({
            method: 'POST',
            url: '/Settings/Add_New_Level',
            data: level,
            dataType: 'json',
            success: function (result) {
                $('#levelTable').prepend("<td><a href='#' id='tbLevelName' onclick='$.getLevelSettingsById(" + result.Id + ")'>" + result.Level_Name + "</a></td>");
                $('#levelName').val("");
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    location.href = "/Members/User_Profile/" + userId;
                }, 5000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------Get Level Settings By Level Id Function----------------------//
$(document).ready(function () {
    $.getLevelSettingsById = function (levelId) {

        if (levelId == 1) {
            $('#afterAlert').prepend("<div id='removeDiv' class='col-md-12' style='color:" + "red" + "'><label>Admin Seviye Ayarları Değiştirilemez!</label></div>");
            $('#afterAlert').show();
            setInterval(function () {
                $('#removeDiv').remove();
                $('#afterAlert').hide();
            }, 2000);

            return;
        }

        var level = {
            id: levelId,
        };

        $.ajax({
            method: 'POST',
            url: '/Settings/Get_LevelSettings_By_Id',
            data: level,
            dataType: 'json',
            success: function (result) {
                $('#levelIdforCompany').val(levelId);
                $('#levelIdforVehicle').val(levelId);
                $('#levelIdforMember').val(levelId);

                $('#addCompany').attr("value", result.Add_Company ? "on" : "off").prop("checked", result.Add_Company).attr("checked", result.Add_Company);
                $('#editCompany').attr("value", result.Edit_Company ? "on" : "off").prop("checked", result.Edit_Company).attr("checked", result.Edit_Company);
                $('#deleteCompany').attr("value", result.Delete_Company ? "on" : "off").prop("checked", result.Edit_Company).attr("checked", result.Edit_Company);
                $('#seeCompanyAccounts').attr("value", result.See_Comp_Accounts ? "on" : "off").prop("checked", result.Edit_Company).attr("checked", result.Edit_Company);

                $('#addVehicle').attr("value", result.Add_Vehicle ? "on" : "off").prop("checked", result.Add_Vehicle).attr("checked", result.Add_Vehicle);
                $('#editVehicle').attr("value", result.Edit_Vehicle ? "on" : "off").prop("checked", result.Edit_Vehicle).attr("checked", result.Edit_Vehicle);
                $('#deleteVehicle').attr("value", result.Delete_Vehicle ? "on" : "off").prop("checked", result.Delete_Vehicle).attr("checked", result.Delete_Vehicle);
                $('#seeVehicleAccounts').attr("value", result.See_Vehicle_Accounts ? "on" : "off").prop("checked", result.See_Vehicle_Accounts).attr("checked", result.See_Vehicle_Accounts);

                $('#addMember').attr("value", result.Add_Member ? "on" : "off").prop("checked", result.Add_Member).attr("checked", result.Add_Member);
                $('#editMember').attr("value", result.Edit_Member ? "on" : "off").prop("checked", result.Edit_Member).attr("checked", result.Edit_Member);
                $('#deleteMember').attr("value", result.Delete_Member ? "on" : "off").prop("checked", result.Delete_Member).attr("checked", result.Delete_Member);
                $('#seeMemberAccounts').attr("value", result.See_Member_Accounts ? "on" : "off").prop("checked", result.See_Member_Accounts).attr("checked", result.See_Member_Accounts);

                location.reload();
            },
            error: function (error) {
                $('#afterAlert').prepend("<div class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
            }
        });
    }
});
//-----------------------------------------------------------------//


//---------------Edit Level Settings Function----------------------//
$(document).ready(function () {
    $.editLevelSettings = function (choose) {

        var levelInfo = {};
        debugger;
        switch (choose) {
            case 1:
                levelInfo = {
                    choose: choose,
                    levelId: $('#levelIdforCompany').val(),
                    addLevel: $('#addCompany').attr("checked") ? true : false,
                    editLevel: $('#editCompany').attr('checked') ? true : false,
                    deleteLevel: $('#deleteCompany').attr('checked') ? true : false,
                    seeAccounts: $('#seeCompAccounts').attr('checked') ? true : false
                }
                break;
            case 2:
                levelInfo = {
                    choose: choose,
                    levelId: $('#levelIdforVehicle').val(),
                    addLevel: $('#addVehicle').attr("checked") ? true : false,
                    editLevel: $('#editVehicle').attr("checked") ? true : false,
                    deleteLevel: $('#deleteVehicle').attr("checked") ? true : false,
                    seeAccounts: $('#seeVehicleAccounts').attr("checked") ? true : false
                }
                break;
            case 3:
                levelInfo = {
                    choose: choose,
                    levelId: $('#levelIdforMember').val(),
                    addLevel: $('#addMember').attr("checked") ? true : false,
                    editLevel: $('#editMember').attr("checked") ? true : false,
                    deleteLevel: $('#deleteMember').attr("checked") ? true : false,
                    seeAccounts: $('#seeMemberAccounts').attr("checked") ? true : false
                }
                bre
            default:
        }
        debugger;
        $.ajax({
            method: 'POST',
            url: '/Settings/Edit_LevelSettings',
            data: levelInfo,
            dataType: 'json',
            success: function (result) {
                $('#afterAlert').prepend("<div id='removeDiv' class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + result + "</label></div>");
                setInterval(function () {
                    $('#removeDiv').remove();
                    $('#afterAlert').hide();
                }, 2000);
            },
            error: function (error) {
                $('#afterAlert').prepend("<div id='removeDiv' class='col-md-12' style='bgcolor:" + "light-green" + "'><label>" + error + "</label></div>");
                setInterval(function () {
                    $('#removeDiv').remove();
                    $('#afterAlert').hide();
                }, 2000);
            }
        });
    }
});
//-----------------------------------------------------------------//

//---------------------------User And Setting Scripts End--------------------------------------------------------------------------------------------------------------//



//---------------------------Authentication And Authorization Scripts Begin-------------------------------------------------------------------------------------------//

//---------------Login Function-----------------------------//
$(document).ready(function () {
    $.login = function () {

        var loginInfo = {
            emailOrTc: $('#emailOrTc').val(),
            password: $('#userPassword').val()
        }
        $.ajax({
            method: 'POST',
            url: '/Authentication/TryLogin',
            data: loginInfo,
            dataType: 'json',
            success: function (result) {
                if (result) {
                    location.href = "/Companies/Index";
                } else {
                    $('#loginAlert').text = "Giriş Bilgisi veya Şifre Yanlış";
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    }
});
//--------------------------------------------------------------//

//---------------Open LockScreen Function-----------------------------//
$(document).ready(function () {
    $.openLockScreen = function () {
        var login = {
            email: $('#userEmail').text(),
            password: $('#userPassword').val()
        }
        $.ajax({
            method: 'POST',
            url: '/Authentication/OpenLockScreen',
            data: login,
            dataType: 'json',
            success: function (result) {
                if (result) {
                    location.href = "/Companies/Index";
                } else {
                    $('#loginAlert').text = "Şifre Yanlış Girdiniz.";
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    }
});
//--------------------------------------------------------------//
//---------------------------Authentication And Authorization Scripts End------------------------------------------------------------------------------------------//