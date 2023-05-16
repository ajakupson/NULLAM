<script>
    import confirm from './../Shared/shared.vue';
    import useVuelidate from '@vuelidate/core';
    import { required, minValue } from '@vuelidate/validators';

    export default {
        setup() {
            return { v$: useVuelidate() }
        },
        components: {
            confirm
        },
        data() {
            return {
                participant: participant
            }
        },
        mounted() {

        },
        methods: {
            saveParticipant() {
                this.setTouched('all');
                if (this.v$.$invalid) return;

                $.ajax({
                    url: "/event/participants/save",
                    type: 'POST',
                    contentType: "application/json",
                    data: JSON.stringify(this.participant),
                    success: function (response) {
                        toastr.success("Osavõtja on edukalt salvestatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        toastr.error(xhr.responseJSON.errMsg, "Viga");
                    }
                });
            },
            setTouched(theModel) {
                if (theModel == 'firstname' || theModel == 'all') { this.v$.participant.Firstname.$touch() }
                if (theModel == 'lastname' || theModel == 'all') { this.v$.participant.Lastname.$touch() }
                if (theModel == 'personalCode' || theModel == 'all') { this.v$.participant.PersonalCode.$touch() }
                if (theModel == 'paymentType' || theModel == 'all') { this.v$.participant.PaymentType.$touch() }
            },
        },
        validations() {
            if (this.participant.Type == 1) {
                return {
                    participant: {
                        Firstname: {
                            required
                        },
                        Lastname: {
                            required
                        },
                        PersonalCode: {
                            required
                        },
                        PaymentType: {
                            required,
                            minValue: minValue(1)
                        }
                    }
                }
            } else {
                return {
                    participant: {
                        Firstname: {
                            required
                        },
                        Lastname: {},
                        PersonalCode: {
                            required
                        },
                        PaymentType: {
                            required,
                            minValue: minValue(1)
                        }
                    }
                }
            }
        },
    }
</script>

<template>
    <div class="div-centered">
        <h1 class="form-title">Osavõtja info</h1>
        <form class="form-main mxw300">
            <div v-if="participant.Type == 1">
                <div class="form-group">
                    <label>Eesnimi:</label>
                    <input type="text" v-model="v$.participant.Firstname.$model" :class="{ error: v$.participant.Firstname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Perenimi:</label>
                    <input type="text" v-model="v$.participant.Lastname.$model" :class="{ error: v$.participant.Lastname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Isikukood:</label>
                    <input type="text" v-model="v$.participant.PersonalCode.$model" :class="{ error: v$.participant.PersonalCode.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Maksmisviis:</label>
                    <select v-model="v$.participant.PaymentType.$model" :class="{ error: v$.participant.PaymentType.$errors.length }">
                        <option value="1">sularaha</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Lisainfo:</label>
                    <textarea v-model="participant.Description"></textarea>
                </div>
            </div>
            <div v-if="participant.Type == 2">
                <div class="form-group">
                    <label>Etevõtte:</label>
                    <input type="text" v-model="v$.participant.Firstname.$model" :class="{ error: v$.participant.Firstname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Kood:</label>
                    <input type="text" v-model="v$.participant.PersonalCode.$model" :class="{ error: v$.participant.PersonalCode.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Maksmisviis:</label>
                    <select v-model="v$.participant.PaymentType.$model" :class="{ error: v$.participant.PaymentType.$errors.length }">
                        <option value="1">sularaha</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Lisainfo:</label>
                    <textarea v-model="participant.Description"></textarea>
                </div>
            </div>
            <div class="form-group flex-jcs">
                <a :href="'/event/' + participant.EventId + '/participants'" class="btn btn-secondary ml0">Tagasi</a>
                <button type="button" class="btn btn-main" @click="saveParticipant">Salvesta</button>
            </div>
        </form>
    </div>
</template>