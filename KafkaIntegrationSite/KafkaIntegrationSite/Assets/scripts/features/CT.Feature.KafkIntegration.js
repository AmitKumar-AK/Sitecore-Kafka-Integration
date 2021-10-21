/****************************************************************************
CT.Feature.kafkaintegration.js has been used for Kafka Management
*****************************************************************************/
/* 
* Version:       0.0.0.1
* Created By:   Amit Kumar
* Created Date:   10/09/2021 (MM/DD/YYYY)
* Modified By:  Amit Kumar
* Modified Date:   10/09/2021 (MM/DD/YYYY)
*/
CT.component.kafkaintegration = (function ($) {
	var api = {},
	cities = null;
    api.returnUrl = "hello";
    //--If you have wildcard topic, that you can define here or populate from server side
	api.topicPreFix = "sitecore-";

    api.pushitemtokafka = function (objTopicName, objMessage) {
        waitingDialog.show('Please Wait..');
        $('#divSuccess').hide();
        $('#divError').hide();

        var objTopicNameValue = $('#' + objTopicName).val();
        var objMessageValue = $('#' + objMessage).val();
        if (!CT.helper.isNullOrEmpty(objTopicNameValue)) {
            objTopicNameValue = api.topicPreFix + objTopicNameValue
        }
        else {
            return;
        }
        if (CT.helper.isNullOrEmpty(objMessageValue)) {
            return;
        }
        var objKafka = new Object();
        objKafka.topic = objTopicNameValue;
        objKafka.message = objMessageValue;
        CT.helper.ajaxPost({ kafkaData: JSON.stringify(objKafka) }, "Kafka/PublishToKafka", CT.helper.ajaxCallType.post, api.getKafkaOnSuccess, api.getKafkaOnError);

    };

    api.pullitemfromkafka = function (objTopicName, objGroupId) {
        waitingDialog.show('Please Wait..');
        $('#divSuccess').hide();
        $('#divError').hide();

        var objTopicNameValue = $('#' + objTopicName).val();
        var objoGroupIdValue = $('#' + objGroupId).val();
        if (!CT.helper.isNullOrEmpty(objTopicNameValue)) {
            objTopicNameValue = api.topicPreFix + objTopicNameValue
        }
        else {
            return;
        }
        if (CT.helper.isNullOrEmpty(objoGroupIdValue)) {
            return;
        }
        var objKafka = new Object();
        objKafka.topic = objTopicNameValue;
        objKafka.groupId = objoGroupIdValue;
        CT.helper.ajaxPost({ kafkaData: JSON.stringify(objKafka) }, "Kafka/PullFromKafka", CT.helper.ajaxCallType.post, api.getKafkaOnSuccess, api.getKafkaOnError);

    };

    api.getKafkaOnSuccess = function (response) {

        if (!CT.helper.isNullOrEmpty(response) && response.result) {

            if (!CT.helper.isNullOrEmpty(response.result) && response.result) {
                CT.helper.logMessage('api.getKafkaOnSuccess -> Success=>' + response.type);
                if (response.type === "add") {
                    //Display success message once item added to Kafka topic
                    $('#divSuccess').html('Message added to Kafka Topic');
                    $('#divSuccess').show();
                    $('#divError').hide();
                }
                else if (response.type === "get") {
                    if (response.value.length > 0) {
                        //Display messages from Kafka topic
                        var getHtml = "";
                        for (var i = 0; i < response.value.length; i++) {
                            getHtml = '</br>' + response.value[i];
                        }
                        $('#divSuccess').html(getHtml);
                        $('#divSuccess').show();
                        $('#divError').hide();
                    }
                }
            }
            else {
                //--If Error
                if (response.type === "add") {
                    CT.helper.logMessage('api.getKafkaOnSuccess ->Error Occured while adding message to Kafka Topic');
                    if (!CT.helper.isNullOrEmpty(response.value) && response.value.length > 0) {
                        var getHtml = "";
                        for (var i = 0; i < response.value.length; i++) {
                            getHtml = '</br>' + response.value[i];
                        }
                        $('#divError').html('Error Occured while adding message to Kafka Topic ' + getHtml);
                        $('#divSuccess').hide();
                        $('#divError').show();
                    }
                }
                else if (response.type === "get") {
                    CT.helper.logMessage('api.getKafkaOnSuccess ->Error Occured while getting message from Kafka Topic');
                    if (!CT.helper.isNullOrEmpty(response.value) && response.value.length > 0) {
                        var getHtml = "";
                        for (var i = 0; i < response.value.length; i++) {
                            getHtml = '</br>' + response.value[i];
                        }
                        $('#divError').html('Error Occured while getting message from Kafka Topic ' + getHtml);
                        $('#divSuccess').hide();
                        $('#divError').show();
                    }
                }
            }
        }
        else {
            //--If error occured during service call
            if (response.type === "add") {
                CT.helper.logMessage('api.getKafkaOnSuccess ->Error Occured while adding message to Kafka Topic');
                if (!CT.helper.isNullOrEmpty(response.value) && response.value.length > 0) {
                    var getHtml = "";
                    for (var i = 0; i < response.value.length; i++) {
                        getHtml = '</br>' + response.value[i];
                    }
                    $('#divError').html('Error Occured while adding message to Kafka Topic ' + getHtml);
                    $('#divSuccess').hide();
                    $('#divError').show();
                }
            }
            else if (response.type === "get") {
                CT.helper.logMessage('api.getKafkaOnSuccess ->Error Occured while getting message from Kafka Topic');
                if (!CT.helper.isNullOrEmpty(response.value) && response.value.length > 0) {
                    var getHtml = "";
                    for (var i = 0; i < response.value.length; i++) {
                        getHtml = '</br>' + response.value[i];
                    }
                    $('#divError').html('Error Occured while getting message from Kafka Topic ' + getHtml);
                    $('#divSuccess').hide();
                    $('#divError').show();
                }
            }
        }
        waitingDialog.hide();
    };

    api.getKafkaOnError = function (response) {
        if (!CT.helper.isNullOrEmpty(response.responseText) && response.responseText.length > 0) {
            CT.helper.logMessage('api.getKafkaOnError -> Error=>' + response.responseText);
            $('#divError').html('Error Occured during service call </br>' + response.responseText);
            $('#divSuccess').hide();
            $('#divError').show();
        }
        else {
            CT.helper.logMessage('api.getKafkaOnError -> Error=>' + response.responseText);
            $('#divError').html('Error Occured during service call </br>' + response.responseText);
            $('#divSuccess').hide();
            $('#divError').show();
        }
        waitingDialog.hide();
    };

	return api;
}(jQuery, document));

CT.register("kafkaintegration", CT.component.kafkaintegration);