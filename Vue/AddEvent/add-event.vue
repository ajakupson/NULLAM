<script>
    import useVuelidate from '@vuelidate/core';
    import { required, minLength } from '@vuelidate/validators';
    import dateFormat, { masks } from "dateformat";

    const dateTimeCheck = (value) => /^(\d{2}).(\d{2}).(\d{4}) (\d{2}):(\d{2})/.test(value);

    export default {
        setup() {
            return { v$: useVuelidate() }
        },
        data() {
            return {
                newEvent: {
                    Name: null,
                    DateTime: null,
                    Place: null,
                    Description: null,
                    IsActive: true
                }
            }
        },
        mounted() {

        },
        methods: {
            addEvent() {
                this.setTouched('all');

                if (!this.v$.$invalid) {
                    let rData = Object.assign({}, this.newEvent);
                    // 2016-01-01T00:00:00.0000000-00:00
                    rData.DateTime = dateFormat(rData.DateTime, "yyyy-mm-dd'T'HH:MM:ss") + ".0000000-00:00";
                    let that = this;
                    $.ajax({
                        url: "/event/add",
                        type: 'POST',
                        contentType: "application/json",
                        data: JSON.stringify(rData),
                        success: function (response) {
                            that.newEvent = {
                                Name: null,
                                DateTime: null,
                                Place: null,
                                Description: null,
                                IsActive: true
                            }

                            toastr.success("Üritus edukalt lisatud", "Edu");
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            toastr.error(xhr.responseJSON.errMsg, "Viga");
                        }
                    });
                }

            },
            setTouched(theModel) {
                if (theModel == 'name' || theModel == 'all') { this.v$.newEvent.Name.$touch() }
                if (theModel == 'dateTime' || theModel == 'all') { this.v$.newEvent.DateTime.$touch() }
                if (theModel == 'place' || theModel == 'all') { this.v$.newEvent.Place.$touch() }
            },
        },
        validations() {
            return {
                newEvent: {
                    Name: {
                        required
                    },
                    DateTime: {
                        required,
                        dateTimeCheck
                    },
                    Place: {
                        required
                    }
                },
            }
        },
    }
</script>

<template>
    <h1 class="form-title">Ürituse lisamine</h1>
    <form class="form-main">
        <div class="form-group">
            <label>Ürituse nimi:</label>
            <input type="text" v-model="v$.newEvent.Name.$model" :class="{ error: v$.newEvent.Name.$errors.length }"/>
        </div>
        <div class="form-group">
            <label>Toimumisaeg:</label>
            <input type="text" v-model="v$.newEvent.DateTime.$model" placeholder="pp.kk.aaaa hh:mm" :class="{ error: v$.newEvent.DateTime.$errors.length }"/>
        </div>
        <div class="form-group">
            <label>Koht:</label>
            <input type="text" v-model="v$.newEvent.Place.$model" :class="{ error: v$.newEvent.Place.$errors.length }"/>
        </div>
        <div class="form-group">
            <label>Lisainfo:</label>
            <textarea v-model="newEvent.Description"></textarea>
        </div>
        <div class="form-group flex-jcs">
            <a href="/" class="btn btn-secondary ml0">Tagasi</a>
            <button type="button" class="btn btn-main" @click="addEvent">Lisa</button>
        </div>
    </form>
</template>