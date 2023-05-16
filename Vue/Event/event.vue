<script>
    import confirm from './../Shared/shared.vue';
    import useVuelidate from '@vuelidate/core';
    import { required, minLength } from '@vuelidate/validators';

    export default {
        setup() {
            return { v$: useVuelidate() }
        },
        components: {
            confirm
        },
        data() {
            return {
                event: event,
                newParticipant: {
                    Firstname: null,
                    Lastanme: null,
                    PersonalCode: null,
                    PaymentType: 1,
                    EventId: event.Id,
                    Type: 1
                },
                selected: null
            }
        },
        mounted() {

        },
        methods: {
            addParticipant() {
                this.setTouched('all');
                if (this.v$.$invalid) return;

                let that = this;
                $.ajax({
                    url: "/event/participants/add",
                    type: 'POST',
                    contentType: "application/json",
                    dataType: 'json',
                    data: JSON.stringify(this.newParticipant),
                    success: function (response) {
                        that.newParticipant.Id = response.id;
                        that.event.Participants.push(Object.assign({}, that.newParticipant));

                        that.newParticipant = {
                            Firstname: null,
                            Lastanme: null,
                            PersonalCode: null,
                            PaymentType: 1,
                            EventId: that.event.Id,
                            Type: 1
                        }

                        toastr.success("Osavõtja on edukalt lisatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        toastr.error(xhr.responseJSON.errMsg, "Viga");
                    }
                });
            },
            confrimRemove: function (participant) {
                this.selected = participant;
                $("#confirm-modal").addClass("shown");
            },
            removeParticipant() {
                let that = this;
                $.ajax({
                    url: "/event/participants/remove",
                    type: 'POST',
                    contentType: "application/json",
                    data: JSON.stringify(this.selected),
                    success: function (response) {
                        that.event.Participants = that.event.Participants.filter(p => p.Id != that.selected.Id);

                        $("#confirm-modal").removeClass("shown");
                        toastr.success("Osavõtja on edukalt kustutatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            setTouched(theModel) {
                if (theModel == 'firstname' || theModel == 'all') { this.v$.newParticipant.Firstname.$touch() }
                if (theModel == 'lastname' || theModel == 'all') { this.v$.newParticipant.Lastname.$touch() }
                if (theModel == 'personalCode' || theModel == 'all') { this.v$.newParticipant.PersonalCode.$touch() }
            },
        },
        validations() {
            if (this.newParticipant.Type == 1) {
                return {
                    newParticipant: {
                        Firstname: {
                            required
                        },
                        Lastname: {
                            required
                        },
                        PersonalCode: {
                            required
                        }
                    }
                }
            } else {
                return {
                    newParticipant: {
                        Firstname: {
                            required
                        },
                        Lastname: {},
                        PersonalCode: {
                            required
                        }
                    }
                }
            }
        },
    }
</script>

<template>
    <div class="div-centered pl20 pr20">
        <h1 class="form-title">Osavõtjad</h1>
        <div class="form-group flex-jcs">
            <label>Ürituse nimi:</label>
            <span>{{ event.Name }}</span>
        </div>
        <div class="form-group flex-jcs">
            <label>Toimumisaeg:</label>
            <span>{{ event.DateTimeFormatted }}</span>
        </div>
        <div class="form-group flex-jcs">
            <label>Koht:</label>
            <span>{{ event.Place }}</span>
        </div>
        <div class="form-group flex-ais">
            <label class="mnw100">Osavõtjad:</label>
            <div class="participants-list mt30">
                <ul>
                    <li v-for="participant in event.Participants">
                        <span>{{ participant.Firstname }} {{ participant.Lastname }}</span>
                        <span>{{ participant.PersonalCode }}</span>
                        <span><a :href="'/event/participants/' + participant.Id" class="btn">Vaata</a></span>
                        <span><a class="btn" @click="confrimRemove(participant)">Kustuta</a></span>
                    </li>
                </ul>
            </div>
        </div>
    </div>


    <div class="div-centered" v-if="event.IsActive">
        <h1 class="form-title">Osavõtjate lisamine</h1>
        <form class="form-main mxw300">
            <div class="form-group">
                <input type="radio" v-model="newParticipant.Type" value="1" name="type" id="person">
                <label for="person">Eraisik</label>
                <input type="radio" v-model="newParticipant.Type" value="2" name="type" id="company">
                <label for="company">Ettevõtte</label>
            </div>
            <div v-if="newParticipant.Type == 1">
                <div class="form-group">
                    <label>Eesnimi:</label>
                    <input type="text" v-model="v$.newParticipant.Firstname.$model" :class="{ error: v$.newParticipant.Firstname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Perenimi:</label>
                    <input type="text" v-model="v$.newParticipant.Lastname.$model" :class="{ error: v$.newParticipant.Lastname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Isikukood:</label>
                    <input type="text" v-model="v$.newParticipant.PersonalCode.$model" :class="{ error: v$.newParticipant.PersonalCode.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Maksmisviis:</label>
                    <select v-model="newParticipant.PaymentType">
                        <option value="1">sularaha</option>
                    </select>
                </div>
            </div>
            <div v-if="newParticipant.Type == 2">
                <div class="form-group">
                    <label>Etevõtte:</label>
                    <input type="text" v-model="v$.newParticipant.Firstname.$model" :class="{ error: v$.newParticipant.Firstname.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Kood:</label>
                    <input type="text" v-model="v$.newParticipant.PersonalCode.$model" :class="{ error: v$.newParticipant.PersonalCode.$errors.length }" />
                </div>
                <div class="form-group">
                    <label>Maksmisviis:</label>
                    <select v-model="newParticipant.PaymentType">
                        <option value="1">sularaha</option>
                    </select>
                </div>
            </div>
            <div class="form-group flex-jcs">
                <a href="/" class="btn btn-secondary ml0">Tagasi</a>
                <button type="button" class="btn btn-main" @click="addParticipant">Salvesta</button>
            </div>
        </form>
    </div>

    <confirm @confirmed="removeParticipant"></confirm>
</template>